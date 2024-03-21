public class Room
{
	private string description;
	private Dictionary<string, Room> exits;
	private List<Enemy> enemies = new List<Enemy>();
	private Inventory chest;
	private bool isLocked;

	public bool Locked { get; private set; }
	public string Key { get; private set; }

	public Room(string description)
	{
		this.description = description;
		exits = new Dictionary<string, Room>();
		chest = new Inventory(999999);
		isLocked = false;
	}

	public void AddEnemy(string name, int health, int attackPower)
	{
		Enemy enemy = new Enemy(name, health, attackPower);
		enemies.Add(enemy);
	}

	public void AddExit(string direction, Room neighbor)
	{
		exits[direction] = neighbor;
	}

	public bool IsLocked()
	{
		return isLocked;
	}

	public string GetShortDescription()
	{
		return description;
	}

	public string GetLongDescription()
	{
		return $"You are {description}.\n{GetExitString()}";
	}

	public Room GetExit(string direction, Inventory playerInventory)
	{
		if (isLocked && !playerInventory.Contains("key"))
		{
			Console.WriteLine("The room is locked. You need a key to unlock it.");
			return null;
		}

		exits.TryGetValue(direction, out Room exit);
		return exit;
	}

	private string GetExitString()
	{
		string returnString = "Exits:";
		foreach (string exit in exits.Keys)
		{
			returnString += " " + exit;
		}
		return returnString;
	}

	public bool AddItem(Item item)
	{
		return chest.AddItem(item);
	}

	public Item RemoveItem(string itemName)
	{
		return chest.RemoveItem(itemName);
	}

	public Dictionary<string, Item> Items
	{
		get { return chest.Items; }
	}

	public Inventory Chest
	{
		get { return chest; }
	}

	public void Lock(string key)
	{
		Locked = true;
		Key = key;
	}

	public bool Unlock(string key)
	{
		if (Locked && key == Key)
		{
			Locked = false;
			return true;
		}
		return false;
	}

	public void Enter(Player player)
	{
		foreach (var enemy in enemies)
		{
			enemy.PlayerEntered(player);
		}
	}

	public void Leave()
	{
		foreach (var enemy in enemies)
		{
			enemy.PlayerLeft();
		}
	}

	public Enemy GetEnemy()
	{
		return enemies.FirstOrDefault();
	}
}