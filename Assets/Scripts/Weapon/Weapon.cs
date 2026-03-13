using System;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public event Action onSuccessfulAttack;

    [SerializeField, Range(1, 100)] protected int damage;
    public int Damage => damage;

    protected float _rangeAttack;
    protected string _originLayer;

    protected abstract string WeaponType { get; }

    protected float _damageModifier;
    protected float _criticalDamageChance;
    protected float _criticalDamageModifier;
    protected float _areaModifier;

    public virtual void Initialize(UnitFaction unitFaction)
    {
        _originLayer = unitFaction.ToString() + WeaponType;
        gameObject.layer = LayerMask.NameToLayer(_originLayer);

        // _rangeAttack = rangeAttack;

        gameObject.SetActive(false);
    }

    public virtual void PrepareAttack(float damageModifier = 1f, float criticalDamageChance = 0f, float criticalDamageModifier = 1f, float areaModifier = 1f)
    {
        _damageModifier = damageModifier;
        _criticalDamageChance = criticalDamageChance;
        _criticalDamageModifier = criticalDamageModifier;
        _areaModifier = areaModifier;

        transform.localScale = new Vector3(1f * areaModifier, 1f * areaModifier, 1f * areaModifier);
    }

    protected bool IsCriticalHit()
    {
        float roll = UnityEngine.Random.Range(0f, 1f);
        return roll < _criticalDamageChance;
    }

    protected void ApplyDamage(IHealth targetUnit)
    {
        int finalDamage = Mathf.RoundToInt(damage * _damageModifier);

        if (IsCriticalHit())
        {
            finalDamage = Mathf.RoundToInt(finalDamage * _criticalDamageModifier);
        }

        targetUnit.TakeDamage(finalDamage);
        onSuccessfulAttack?.Invoke();
    }
}