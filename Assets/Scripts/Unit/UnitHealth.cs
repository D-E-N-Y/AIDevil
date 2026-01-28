using System;

public class UnitHealth : IHealth
{
    private UnitStats _stats;
    
    private int _currentHP;
    private int _maxHP;
    private float _armor;

    public UnitHealth(UnitStats stats)
    {
        _stats = stats;
        
        SetStats();
        _currentHP = _maxHP;

        _stats.OnStatChanged += UpdateStats;
    }

    private void UpdateStats(StatType statType)
    {
        if (statType == StatType.MaxHP || statType == StatType.Armor)
        {
            SetStats();
        }
    }

    private void SetStats()
    {
        _maxHP = _stats.MaxHP;
        _armor = _stats.Armor;
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
}