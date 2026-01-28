public class ItemContext
{
    public PlayerCharacter Owner;
    public UnitStats Stats;
    public Inventory Inventory;

    public ItemContext(PlayerCharacter owner, UnitStats stats, Inventory inventory)
    {
        Owner = owner;
        Stats = stats;
        Inventory = inventory;
    }
}