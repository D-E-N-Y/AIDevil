public class SpellContext
{
    public UnitFaction UnitFaction;
    public UnitStats Stats;
    public UnitHealth UnitHealth;
    public IUnitMovement Movement;

    public SpellContext(
        UnitFaction unitFaction,
        UnitStats stats,
        UnitHealth unitHealth, 
        IUnitMovement movement)
    {
        UnitFaction = unitFaction;
        Stats = stats;
        UnitHealth = unitHealth;
        Movement = movement;
    }
}