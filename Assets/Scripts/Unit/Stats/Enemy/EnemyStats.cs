using System;
using UnityEngine;

[Serializable]
public class EnemyStats : UnitStats
{
    [SerializeField, Range(0, 1000)] private int dropMoney;
    public int DropMoney => dropMoney;
}