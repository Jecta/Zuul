public class Item
{
    public int Weight { get; }
    public string Description { get; }
    public bool Usability { get; }
    public int Damage { get; }

    public Item(int weight, string description, bool usability = false, int damage = 0)
    {
        Weight = weight;
        Description = description;
        Usability = usability;
        Damage = damage;
    }
}