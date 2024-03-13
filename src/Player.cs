public class Player
{
    private int health;
    public Room CurrentRoom { get; set; }
    public Inventory Inventory { get; private set; }

    public Player()
    {
        CurrentRoom = null;
        health = 100;
        Inventory = new Inventory(25);
    }

    public void Damage(int amount)
    {
        health = Math.Max(health - amount, 0);
    }

    public void Heal(int amount)
    {
        health = Math.Min(health + amount, 100);
    }

    public bool IsAlive()
    {
        return health > 0;
    }

    public int GetHealth()
    {
        return health;
    }

    public bool TakeFromChest(string itemName)
    {
        if (CurrentRoom == null)
        {
            Console.WriteLine("There is no current room.");
            return false;
        }

        Item item = CurrentRoom.RemoveItem(itemName);
        if (item == null)
        {
            Console.WriteLine("The item does not exist in the room.");
            return false;
        }

        if (!Inventory.AddItem(item))
        {
            Console.WriteLine("The item does not fit in your backpack.");
            CurrentRoom.AddItem(item);
            return false;
        }

        Console.WriteLine("You took the item: " + itemName);
        return true;
    }

    public bool DropToChest(string itemName)
    {
        if (CurrentRoom == null)
        {
            Console.WriteLine("There is no current room.");
            return false;
        }

        Item item = Inventory.RemoveItem(itemName);
        if (item == null)
        {
            Console.WriteLine("The item does not exist in your backpack.");
            return false;
        }

        if (!CurrentRoom.AddItem(item))
        {
            Console.WriteLine("The item cannot be added to the room.");
            Inventory.AddItem(item);
            return false;
        }

        Console.WriteLine("You dropped the item: " + itemName);
        return true;
    }
}
