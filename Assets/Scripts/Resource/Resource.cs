using System;
using UnityEngine;

public class Resource : MonoBehaviour, IDamagable
{
    [SerializeField] private ResourceType _type;
    public ResourceType Type => _type;

    [SerializeField, Range(1, 1000)] private int _amount;
    [SerializeField, Range(1, 1000)] private int _spread;

    [SerializeField, Range(1, 1000)] private int _maxHP;

    [SerializeField] protected WorldResource worldResourcePrefab;

    [SerializeField] private UI_HPIndicator ui_hpIndicator;

    public event Action<IDamagable> OnDead;

    private ResourceHealth _health;

    public void Initialize()
    {
        _health = new ResourceHealth(_maxHP);
        _health.OnDead += Death;

        ui_hpIndicator.Initialize(_health);
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
        WorldResource worldResource = Instantiate(worldResourcePrefab, transform.position, Quaternion.identity);
        worldResource.Initialize(_type, GetAmount());
        
        OnDead?.Invoke(this);
        gameObject.SetActive(false);
    }

    public IHealth GetHealth() => _health;
    public Transform GetTransform() => transform;    
}