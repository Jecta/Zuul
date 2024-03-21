class Game
{
	private Parser parser;
	private Room currentRoom;
	private Player player;
	private Item knife;
	private Item key;

	public Game()
	{
		parser = new Parser();
		player = new Player();
		CreateItems();
		CreateRooms();
	}

	private void CreateItems()
	{
		knife = new Item(10, "knife", true, 10);
		key = new Item(5, "key", false, 0);
	}

	private void CreateRooms()
	{
		Room outside = new Room("outside the main entrance of the university");
		Room theatre = new Room("in a lecture theatre");
		Room pub = new Room("in the campus pub");
		Room lab = new Room("in a computing lab");
		Room office = new Room("in the computing admin office");

		outside.AddExit("east", theatre);
		outside.AddExit("south", lab);
		outside.AddExit("west", pub);

		theatre.AddExit("west", outside);
		theatre.AddEnemy("Anonymous Person", 100, 10);

		pub.AddExit("east", outside);

		lab.AddItem(key);
		lab.AddExit("north", outside);
		lab.AddExit("east", office);

		office.AddExit("west", lab);
		office.Lock("key");
		office.AddItem(knife);

		currentRoom = outside;
		player.CurrentRoom = outside;
	}

	public void Play()
	{
		PrintWelcome();

		bool finished = false;
		while (!finished)
		{
			Command command = parser.GetCommand();
			finished = ProcessCommand(command);
			if (!player.IsAlive())
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("\nYou have died. Game over.");
				Console.ResetColor();
				finished = true;
			}
		}
		Console.WriteLine("");
		Console.WriteLine("Thank you for playing.");
		Console.ForegroundColor = ConsoleColor.Red;
		Console.WriteLine("Press [Enter] to close the game...");
		Console.ResetColor();
		Console.ReadLine();
	}

	private void PrintWelcome()
	{
		Console.WriteLine();
		Console.ForegroundColor = ConsoleColor.Red;
		Console.WriteLine("__________           .__   ");
		Console.WriteLine("\\____    /__ __ __ __|  |  ");
		Console.WriteLine("  /     /|  |  \\  |  \\  |  ");
		Console.WriteLine(" /     /_|  |  /  |  /  |__");
		Console.WriteLine("/_______ \\____/|____/|____/");
		Console.WriteLine("        \\/                 ");
		Console.ResetColor();
		Console.WriteLine("Zuul is a new, incredibly boring adventure game.");
		Console.ForegroundColor = ConsoleColor.Green;
		Console.WriteLine("Type 'help' to see what your commands are.");
		Console.ResetColor();
		Console.WriteLine();
		Console.WriteLine(currentRoom.GetLongDescription());
	}

	private bool ProcessCommand(Command command)
	{
		bool wantToQuit = false;

		if (command.IsUnknown())
		{
			Console.WriteLine("I don't know what you mean. Try 'help'.");
			return wantToQuit;
		}

		switch (command.CommandWord)
		{
			case "help":
				PrintHelp();
				break;
			case "go":
				GoRoom(command);
				break;
			case "quit":
				wantToQuit = true;
				break;
			case "look":
				Look(command);
				break;
			case "status":
				Status(command);
				break;
			case "take":
				Take(command);
				break;
			case "drop":
				Drop(command);
				break;
			case "use":
				Use(command);
				break;
			case "attack":
				Attack(command);
				break;
		}

		return wantToQuit;
	}

	private void PrintHelp()
	{
		Console.WriteLine("You are lost and alone.");
		Console.WriteLine("You wander around at the university.");
		Console.WriteLine("You're bleading every step you take. You need to find a way to stop the bleeding or restore your health.");
		Console.WriteLine();
		parser.PrintValidCommands();
	}

	private void GoRoom(Command command)
	{
		if (!command.HasSecondWord())
		{
			Console.WriteLine("Where do you want to go? Use 'go' followed by a direction.");
			Console.WriteLine("Go where?");
			return;
		}

		string direction = command.SecondWord;

		Room nextRoom = currentRoom.GetExit(direction, player.Inventory);

		if (nextRoom == null)
		{
			Console.WriteLine("There is nothing there...");
		}
		else if (nextRoom.Locked)
		{
			if (player.Inventory.Contains("key"))
			{
				currentRoom = nextRoom;
				nextRoom.Unlock(nextRoom.Key);
				player.CurrentRoom.Leave();
				player.CurrentRoom = nextRoom;
				nextRoom.Enter(player);
				Console.WriteLine(currentRoom.GetLongDescription());
				player.Damage(10);
			}
			else
			{
				Console.WriteLine("The room is locked. You need to find a key to enter.");
			}
		}
		else
		{
			currentRoom = nextRoom;
			player.CurrentRoom.Leave();
			player.CurrentRoom = nextRoom;
			nextRoom.Enter(player);
			Console.WriteLine(currentRoom.GetLongDescription());
			player.Damage(10);
		}
	}

	private void Look(Command command)
	{
		Console.WriteLine(currentRoom.GetLongDescription());
		if (currentRoom.Items.Count > 0)
		{
			Console.Write("You see ");
			bool firstItem = true;
			foreach (var item in currentRoom.Items.Values)
			{
				if (!firstItem)
				{
					Console.Write(", ");
				}
				Console.Write("a " + item.Description);
				firstItem = false;
			}
			Console.WriteLine();
		}
	}

	private void Status(Command command)
	{
		Console.WriteLine("Health: " + player.GetHealth());
		Console.WriteLine();
		Console.WriteLine("Player Inventory " + "(free weight: " + player.Inventory.FreeWeight() + ")" + ":");

		if (player.Inventory.Items.Count == 0)
		{
			Console.WriteLine("Inventory is empty");
		}
		else
		{
			foreach (var item in player.Inventory.Items)
			{
				Console.WriteLine(item.Value.Description + " (" + item.Value.Weight + " weight)");
			}
		}
	}

	private void Take(Command command)
	{
		if (!command.HasSecondWord())
		{
			Console.WriteLine("What do you want to take?");
			return;
		}

		string item = command.SecondWord;

		if (currentRoom.Items.ContainsKey(item))
		{
			Item itemToTake = currentRoom.RemoveItem(item);
			player.Inventory.AddItem(itemToTake);
			Console.WriteLine("You took the " + item);
		}
		else
		{
			Console.WriteLine("There is no " + item + " in this room");
		}
	}
	private void Drop(Command command)
	{
		if (!command.HasSecondWord())
		{
			Console.WriteLine("What do you want to dropped?");
			return;
		}

		string item = command.SecondWord;

		if (player.Inventory.Contains(item))
		{
			Item removedItem = player.Inventory.RemoveItem(item);
			currentRoom.Items.Add(item, removedItem);
			Console.WriteLine("You dropped " + item);
		}
		else
		{
			Console.WriteLine("You don't have " + item);
		}
	}

	private void Use(Command command)
	{
		if (!command.HasSecondWord())
		{
			Console.WriteLine("What do you want to use?");
			return;
		}

		string item = command.SecondWord;

		if (!command.HasThirdWord())
		{
			if (player.Inventory.Contains(item))
			{
				Console.WriteLine("You tried to use " + item + " but there is nothing to use it on");
			}
			else
			{
				Console.WriteLine("You don't have " + item);
			}
		}
		else
		{
			if (!player.Inventory.Contains(item))
			{
				Console.WriteLine("You don't have " + item);
			}
			else
			{
				string target = command.ThirdWord;
				Console.WriteLine("There is nothing at " + target + " to use " + item + " on");
			}
		}
	}

	private void Attack(Command command)
	{
		var enemy = player.CurrentRoom.GetEnemy();
		if (enemy != null)
		{
			if (player.Inventory.Contains("knife"))
			{
				enemy.DamageEnemy(knife.Damage);
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("You attacked " + enemy.Name + " for " + knife.Damage + " damage");
				Console.ResetColor();
			}
			else
			{
				Console.WriteLine("You need a knife to attack");
			}
		}
		else
		{
			Console.WriteLine("There is no enemy in this room to attack.");
		}
	}

}