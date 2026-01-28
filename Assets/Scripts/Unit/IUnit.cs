using System;

public interface IUnit
{
    event Action<IUnit> OnDead;
    
    string GetName();
    UnitStats GetStats();
    UnitHealth GetHealth();
}