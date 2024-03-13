public class Room
{
	private string description;
	private Dictionary<string, Room> exits;
	private Inventory chest;

	public Room(string description)
	{
		this.description = description;
		exits = new Dictionary<string, Room>();
		chest = new Inventory(999999);
	}

	public void AddExit(string direction, Room neighbor)
	{
		exits[direction] = neighbor;
	}

	public string GetShortDescription()
	{
		return description;
	}

	public string GetLongDescription()
	{
		return $"You are {description}.\n{GetExitString()}";
	}

	public Room GetExit(string direction)
	{
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
}