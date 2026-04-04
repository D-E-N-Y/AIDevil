public class UnitContext
{
    public UnitStats Stats;
    public Inventory Inventory;
    public SpellController SpellController;
    public UnitHealth UnitHealth;
    public Wallet Wallet;
    public IUnitMovement Movement;

    public UnitContext(
        UnitStats stats, 
        Inventory inventory, 
        SpellController spellController, 
        UnitHealth unitHealth, 
        Wallet wallet,
        IUnitMovement movement)
    {
        Stats = stats;
        Inventory = inventory;
        SpellController = spellController;
        UnitHealth = unitHealth;
        Wallet = wallet;
        Movement = movement;
    }
}