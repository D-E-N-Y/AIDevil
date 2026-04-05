using System;

public interface IUnit
{
    string GetName();
    UnitStats GetStats();
    UnitHealth GetHealth();
}