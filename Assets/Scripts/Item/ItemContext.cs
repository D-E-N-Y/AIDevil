public class ItemContext
{
    public UnitStats Stats;
    public Inventory Inventory;
    public SpellController SpellController;
    public UnitHealth UnitHealth;
    public Wallet Wallet;

    public ItemContext(UnitStats stats, Inventory inventory, SpellController spellController, UnitHealth unitHealth, Wallet wallet)
    {
        Stats = stats;
        Inventory = inventory;
        SpellController = spellController;
        UnitHealth = unitHealth;
        Wallet = wallet;
    }
}