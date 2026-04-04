using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealth : IHealth
{
    private UnitStats _stats;
    
    private int _currentHP;
    private int _maxHP;
    private float _armor;
    private float _dodgeChance;

    private Dictionary<StatType, Action> _updateStats;

    private bool _isInvulnerable;
    public bool IsInvulnerable => _isInvulnerable;

    public UnitHealth(UnitStats stats)
    {
        _stats = stats;
        
        _isInvulnerable = false;

        _updateStats = new Dictionary<StatType, Action>()
        {
            {StatType.MaxHP, SetMaxHP},
            {StatType.Armor, SetArmor},
            {StatType.DodgeChance, SetDodgeChance}  
        };
        
        _maxHP = _stats.MaxHP;
        _armor = _stats.Armor;
        _dodgeChance = _stats.DodgeChance;
        
        _currentHP = _maxHP;

        _stats.OnStatChanged += UpdateStats;
    }

    private void UpdateStats(StatType statType)
    {
        if(_updateStats.ContainsKey(statType))
        {
            _updateStats[statType]?.Invoke();
        }
    }

    private void SetMaxHP()
    {
        int _heal = _stats.MaxHP - _maxHP;

        _maxHP = _stats.MaxHP;
        
        Heal(_heal);

        if(_currentHP > _maxHP)
        {
            _currentHP = _maxHP;
            OnHpChanged?.Invoke();
        }
    }

    private void SetArmor()
    {
        _armor = _stats.Armor;
    }

    private void SetDodgeChance()
    {
        _dodgeChance = _stats.DodgeChance;
    }
    
    public int CurrentHP => _currentHP;
    public int MaxHP => _maxHP;

    public event Action OnHpChanged;
    public event Action OnDead;

    public void Heal(int value)
    {
        value = Math.Max(0, value);
        _currentHP = Math.Min(_currentHP + value, _maxHP);

        OnHpChanged?.Invoke();
    }

    public void TakeDamage(float value)
    { 
        if (_isInvulnerable) return;

        // dodge damage
        float dodgeRoll = UnityEngine.Random.Range(0f, 1f);
        if (dodgeRoll < _dodgeChance) return;

        value = Math.Max(0, value);
        float reduction = _armor / (_armor + 100f);
        float damage = value * (1f - reduction);

        _currentHP -= (int)damage;

        OnHpChanged?.Invoke();

        if (_currentHP <= 0)
        {
            OnDead?.Invoke();
        }
    }

    public void SetInvulnerability(bool value)
    {
        _isInvulnerable = value;
    }
}