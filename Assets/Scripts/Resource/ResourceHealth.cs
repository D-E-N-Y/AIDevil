using System;

public class ResourceHealth : IHealth
{
    public event Action OnHpChanged;
    public event Action OnDead;

    private int _currentHP;
    private int _maxHP;

    public ResourceHealth(int maxHP)
    {
        _maxHP = maxHP;
        _currentHP = _maxHP;
    }

    public void TakeDamage(float value)
    {
        value = Math.Max(0, value);
        _currentHP -= (int)value;

        OnHpChanged?.Invoke();

        if (_currentHP <= 0)
        {
            OnDead?.Invoke();
        }
    }

    public void Heal(int value)
    {
        value = Math.Max(0, value);
        _currentHP = Math.Min(_currentHP + value, _maxHP);

        OnHpChanged?.Invoke();
    }

    public int CurrentHP => _currentHP;
    public int MaxHP => _maxHP;
}