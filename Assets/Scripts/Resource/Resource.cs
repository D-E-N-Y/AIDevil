using System;
using UnityEngine;

public class Resource : MonoBehaviour, IDamagable
{
    [SerializeField] private ResourceType _type;
    public ResourceType Type => _type;

    [SerializeField, Range(1, 1000)] private int _amount;
    [SerializeField, Range(1, 1000)] private int _spread;

    [SerializeField, Range(1, 1000)] private int _maxHP;

    [SerializeField] private UI_HPIndicator ui_hpIndicator;

    public event Action<IDamagable> OnDead;
    
    private bool _isDead;
    public bool IsDead => _isDead;

    private ResourceHealth _health;

    public int Amount { get; private set; }

    public void Initialize()
    {
        _health = new ResourceHealth(_maxHP);
        _health.OnDead += Death;

        _isDead = false;

        ui_hpIndicator.Initialize(_health);

        Amount = GetAmount();

        gameObject.SetActive(true);
    }

    private int GetAmount()
    {
        int min = Math.Max(1, _amount - _spread);
        int max = _amount + _spread;

        int finalAmount = UnityEngine.Random.Range(min, max);

        return finalAmount;
    }

    public void Death()
    {
        _isDead = true;

        OnDead?.Invoke(this);
        gameObject.SetActive(false);
    }

    public IHealth IHealth => _health;    
}