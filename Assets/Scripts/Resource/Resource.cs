using System;
using UnityEngine;

public class Resource : MonoBehaviour, IHealth
{
    [SerializeField] private ResourceType _type;
    public ResourceType Type => _type;

    [SerializeField, Range(1, 1000)] private int _amount;
    [SerializeField, Range(1, 1000)] private int _spread;

    private int _currentHP;
    public int CurrentHP => _currentHP;

    [SerializeField, Range(1, 1000)] private int _maxHP;
    public int MaxHP => _maxHP;

    [SerializeField] protected WorldResource worldResourcePrefab;

    [SerializeField] private UI_HPIndicator ui_hpIndicator;

    public event Action OnHpChanged;
    public event Action OnDead;

    public void Initialize()
    {
        _currentHP = _maxHP;

        ui_hpIndicator.Initialize(this);
        
        Debug.Log($"Initialize {_type} {_currentHP}");
    }

    private int GetAmount()
    {
        int min = Math.Max(1, _amount - _spread);
        int max = _amount + _spread;

        int finalAmount = UnityEngine.Random.Range(min, max);

        return finalAmount;
    }

    public void TakeDamage(float value)
    {
        Debug.Log($"{_type} get dagame {value}");
        
        value = Math.Max(0, value);
        _currentHP -= (int)value;

        OnHpChanged?.Invoke();

        if (_currentHP <= 0)
        {
            Death();
        }
    }

    public void Heal(int value)
    {
        value = Math.Max(0, value);
        _currentHP = Math.Min(_currentHP + value, _maxHP);

        OnHpChanged?.Invoke();
    }

    public void Death()
    {
        WorldResource worldResource = Instantiate(worldResourcePrefab, transform.position, Quaternion.identity);
        worldResource.Initialize(_type, GetAmount());

        gameObject.SetActive(false);

        OnDead?.Invoke();
    }
}