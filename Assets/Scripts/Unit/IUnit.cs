using System;

public interface IUnit
{
    string Name { get; }
    UnitStats Stats { get; }
    UnitHealth Health { get; }
}