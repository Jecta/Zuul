public class Inventory
{
    private int maxWeight;
    private Dictionary<string, Item> items;

    public Inventory(int maxWeight)
    {
        this.maxWeight = maxWeight;
        items = new Dictionary<string, Item>();
    }

    public bool AddItem(Item item)
    {
        if (item.Weight <= FreeWeight())
        {
            items[item.Description] = item;
            return true;
        }
        
        return false;
    }

    public Item RemoveItem(string itemName)
    {
        if (items.ContainsKey(itemName))
        {
            Item item = items[itemName];
            items.Remove(itemName);
            return item;
        }
        
        return null;
    }

    public int TotalWeight()
    {
        int total = 0;
        foreach (Item item in items.Values)
        {
            total += item.Weight;
        }
        return total;
    }

    public int FreeWeight()
    {
        return maxWeight - TotalWeight();
    }

    public Dictionary<string, Item> Items => items;

    public bool Contains(string itemName)
    {
        return items.ContainsKey(itemName);
    }
}