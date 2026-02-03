using System;
using UnityEngine;

[Serializable]
public class UnitStats 
{
    public event Action OnStatsChanged;
    public event Action<StatType> OnStatChanged;
    
    [SerializeField, Range(1, 1000)] protected int maxHP;
    public int MaxHP => maxHP;

    [SerializeField, Range(1f, 10f)] private float baseMoveSpeed;
    public float BaseMoveSpeed => baseMoveSpeed;

    [SerializeField, Range(0.1f, 1f)] private float moveSpeedModifier;
    public float MoveSpeedModifier => moveSpeedModifier;
    
    [SerializeField, Range(1, 1000)] private float armor;
    public float Armor => armor;
    
    [SerializeField, Range(0f, 1f)] private float damageModifier;
    public float DamageModifier => damageModifier;

    [SerializeField, Range(0f, 1f)] private float speedAttackModifier;
    public float SpeedAttackModifier => speedAttackModifier;

    [SerializeField, Range(0f, 1f)] private float criticalDamageChance;
    public float CriticalDamageChance => criticalDamageChance;

    [SerializeField, Range(0f, 1f)] private float criticalDamageModifier;
    public float CriticalDamageModifier => criticalDamageModifier;

    [SerializeField, Range(0f, 1f)] private float multiattackChance;
    public float MultiattackChance => multiattackChance;

    [SerializeField, Range(0f, 1f)] private float areaModifier;
    public float AreaModifier => areaModifier;

    [SerializeField, Range(0f, 1f)] private float dodgeChance;
    public float DodgeChance => dodgeChance;

    public void ModifyMaxHP(int value)
    {
        int _maxHP = maxHP + value;
        maxHP = Math.Max(1, _maxHP);

       RaiseStatChanged(StatType.MaxHP);
    }

    public void ModifyBaseMoveSpeed(float value)
    {
        float _baseMoveSpeed = baseMoveSpeed + value;
        baseMoveSpeed = Math.Max(1f, _baseMoveSpeed);

        RaiseStatChanged(StatType.BaseMoveSpeed);
    }

    public void ModifyMoveSpeedModifier(float value)
    {
        float _moveSpeedModifier = moveSpeedModifier + value;
        moveSpeedModifier = Math.Max(0.1f, _moveSpeedModifier);

        RaiseStatChanged(StatType.MoveSpeedModifier);
    }

    public void ModifyArmor(float value)
    {
        float _armor = armor + value;
        armor = Math.Max(0f, _armor);

        RaiseStatChanged(StatType.Armor);
    }

    public void ModifyDamageModifier(float value)
    {
        float _damageModifier = damageModifier + value;
        damageModifier = Math.Max(0.1f, _damageModifier);

        RaiseStatChanged(StatType.DamageModifier);
    }

    public void ModifySpeedAttackModifier(float value)
    {
        float _speedAttackModifier = speedAttackModifier + value;
        speedAttackModifier = Math.Max(0.1f, _speedAttackModifier);

        RaiseStatChanged(StatType.SpeedAttackModifier);
    }

    public void ModifyCriticalDamageChance(float value)
    {
        float _criticalDamageChance = criticalDamageChance + value;
        criticalDamageChance = Math.Max(0f, _criticalDamageChance);

        RaiseStatChanged(StatType.CriticalDamageChance);
    }

    public void ModifyCriticalDamageModifier(float value)
    {
        float _criticalDamageModifier = criticalDamageModifier + value;
        criticalDamageModifier = Math.Max(0.1f, _criticalDamageModifier);

        RaiseStatChanged(StatType.CriticalDamageModifier);
    }

    public void ModifyMultiattackChance(float value)
    {
        float _multiattackChance = multiattackChance + value;
        multiattackChance = Math.Max(0f, _multiattackChance);

        RaiseStatChanged(StatType.MultiattackChance);
    }

    public void ModifyAreaModifier(float value)
    {
        float _areaModifier = areaModifier + value;
        areaModifier = Math.Max(0.1f, _areaModifier);

        RaiseStatChanged(StatType.AreaModifier);
    }

    public void ModifyDodgeChance(float value)
    {
        float _dodgeChance = dodgeChance + value;
        dodgeChance = Math.Max(0f, _dodgeChance);

        RaiseStatChanged(StatType.DodgeChance);
    }

    protected void RaiseStatChanged(StatType stat)
    {
        OnStatChanged?.Invoke(stat);
        OnStatsChanged?.Invoke();
    }
}