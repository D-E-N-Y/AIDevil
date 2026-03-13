using System;
using UnityEngine;

[Serializable, CreateAssetMenu(fileName = "Enemy Stats", menuName = "UnitStats/EnemyStats")]
public class EnemyStats : UnitStats
{
    [SerializeField, Range(0, 1000)] private int dropMoney;
    public int DropMoney => dropMoney;

    public override void Initialize()
    {
        base.Initialize();

        _modifyStat.Add(StatType.DropMoney, ModifyDropMoney);
        
        _currentStats.Add(StatType.DropMoney, DropMoney);
    }

    protected override void UpdateStarts()
    {
        base.UpdateStarts();

        _currentStats[StatType.DropMoney] = DropMoney;
    }

    private void ModifyDropMoney(float value)
    {
        int _dropMoney = dropMoney + Mathf.RoundToInt(value);
        dropMoney = Mathf.Max(0, _dropMoney);
    }
}