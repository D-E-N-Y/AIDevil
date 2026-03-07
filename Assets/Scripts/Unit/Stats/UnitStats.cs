using System;
using System.Collections.Generic;
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

    protected Dictionary<StatType, Action<float>> _modifyStat;
    
    protected Dictionary<StatType, float> _currentStats;
    public IReadOnlyDictionary<StatType, float> CurrentStats => _currentStats;

    public virtual void Initialize()
    {
        _modifyStat = new Dictionary<StatType, Action<float>>
        {
            { StatType.MaxHP, ModifyMaxHP },
            { StatType.BaseMoveSpeed, ModifyBaseMoveSpeed },
            { StatType.MoveSpeedModifier, ModifyMoveSpeedModifier },
            { StatType.Armor, ModifyArmor },
            { StatType.DamageModifier, ModifyDamageModifier },
            { StatType.SpeedAttackModifier, ModifySpeedAttackModifier },
            { StatType.CriticalDamageChance, ModifyCriticalDamageChance },
            { StatType.CriticalDamageModifier, ModifyCriticalDamageModifier },
            { StatType.MultiattackChance, ModifyMultiattackChance },
            { StatType.AreaModifier, ModifyAreaModifier },
            { StatType.DodgeChance, ModifyDodgeChance }
        };

        _currentStats = new Dictionary<StatType, float>
        {
            { StatType.MaxHP, maxHP },
            { StatType.BaseMoveSpeed, baseMoveSpeed },
            { StatType.MoveSpeedModifier, moveSpeedModifier },
            { StatType.Armor, armor },
            { StatType.DamageModifier, damageModifier },
            { StatType.SpeedAttackModifier, speedAttackModifier },
            { StatType.CriticalDamageChance, criticalDamageChance },
            { StatType.CriticalDamageModifier, criticalDamageModifier },
            { StatType.MultiattackChance, multiattackChance },
            { StatType.AreaModifier, areaModifier },
            { StatType.DodgeChance, dodgeChance }
        };
    }

    public virtual void ModifyStat(StatType stat, float value)
    {
        if (_modifyStat.TryGetValue(stat, out var modifier))
        {
            modifier.Invoke(value);
            RaiseStatChanged(stat);
        }
        else
        {
            Debug.LogWarning($"No modifier registered for stat {stat}");
        }
    }

    private void ModifyMaxHP(float value)
    {
        int _maxHP = maxHP + (int)value;
        maxHP = Math.Max(1, _maxHP);
    }

    private void ModifyBaseMoveSpeed(float value)
    {
        float _baseMoveSpeed = baseMoveSpeed + value;
        baseMoveSpeed = Math.Max(1f, _baseMoveSpeed);
    }

    private void ModifyMoveSpeedModifier(float value)
    {
        float _moveSpeedModifier = moveSpeedModifier + value;
        moveSpeedModifier = Math.Max(0.1f, _moveSpeedModifier);
    }

    private void ModifyArmor(float value)
    {
        float _armor = armor + value;
        armor = Math.Max(0f, _armor);
    }

    private void ModifyDamageModifier(float value)
    {
        float _damageModifier = damageModifier + value;
        damageModifier = Math.Max(0.1f, _damageModifier);
    }

    private void ModifySpeedAttackModifier(float value)
    {
        float _speedAttackModifier = speedAttackModifier + value;
        speedAttackModifier = Math.Max(0.1f, _speedAttackModifier);
    }

    private void ModifyCriticalDamageChance(float value)
    {
        float _criticalDamageChance = criticalDamageChance + value;
        criticalDamageChance = Math.Max(0f, _criticalDamageChance);
    }

    private void ModifyCriticalDamageModifier(float value)
    {
        float _criticalDamageModifier = criticalDamageModifier + value;
        criticalDamageModifier = Math.Max(0.1f, _criticalDamageModifier);
    }

    private void ModifyMultiattackChance(float value)
    {
        float _multiattackChance = multiattackChance + value;
        multiattackChance = Math.Max(0f, _multiattackChance);
    }

    private void ModifyAreaModifier(float value)
    {
        float _areaModifier = areaModifier + value;
        areaModifier = Math.Max(0.1f, _areaModifier);
    }

    private void ModifyDodgeChance(float value)
    {
        float _dodgeChance = dodgeChance + value;
        dodgeChance = Math.Max(0f, _dodgeChance);
    }

    protected void RaiseStatChanged(StatType stat)
    {
        OnStatChanged?.Invoke(stat);
        OnStatsChanged?.Invoke();
    }
}