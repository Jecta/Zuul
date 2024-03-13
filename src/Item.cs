public class Item
{
    public int Weight { get; }
    public string Description { get; }

    public Item(int weight, string description) => (Weight, Description) = (weight, description);
}