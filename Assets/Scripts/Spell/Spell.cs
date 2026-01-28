using System;
using System.Collections;
using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    public Action<float> updateCooldown;

    public Action onStartCooldown;
    public Action onStopCooldown;

    public Action onStartAttack;
    public Action onAttack;
    public Action onStopAttack;

    public Action onSuccessfulAttack;
    public bool IsAttacking { get; protected set; }

    [SerializeField, Range(0.1f, 100f)] protected float rangeAttack;

    protected UnitFaction _unitFaction;
    [SerializeField, Range(0.1f, 15f)] protected float cooldown;
    
    protected float _damageModifier;
    public float DamageModifier => _damageModifier;
    protected float _speedAttackModifier;
    public float SpeedAttackModifier => _speedAttackModifier;
    protected float _criticalDamageChance;
    public float CriticalDamageChance => _criticalDamageChance;
    protected float _criticalDamageModifier;
    public float CriticalDamageModifier => _criticalDamageModifier;
    protected float _multiattackChance;
    public float MultiattackChance => _multiattackChance;
    protected float _areaModifier;
    public float AreaModifier => _areaModifier;


    protected Coroutine attacking;

    [SerializeField] private Sprite icon;

    protected UnitStats _stats;

    public virtual void Initialize(UnitFaction unitFaction, UnitStats stats)
    {
        attacking = null;
        _unitFaction = unitFaction;

        _stats = stats;
        SetStats();
    }

    protected void UpdateStats(StatType statType)
    {
        if(statType == StatType.SpeedAttackModifier || 
           statType == StatType.DamageModifier ||
           statType == StatType.CriticalDamageChance || 
           statType == StatType.CriticalDamageModifier ||
           statType == StatType.MultiattackChance || 
           statType == StatType.AreaModifier)
        {
            SetStats();
        }
    }

    protected virtual void SetStats()
    {
        _damageModifier = _stats.DamageModifier;
        _speedAttackModifier = _stats.SpeedAttackModifier;
        _criticalDamageChance = _stats.CriticalDamageChance;
        _criticalDamageModifier = _stats.CriticalDamageModifier;
        _multiattackChance = _stats.MultiattackChance;
        _areaModifier = _stats.AreaModifier;
    }

    public abstract void Cast();
    protected abstract IEnumerator Attacking();
    protected abstract IEnumerator Attack();
    protected virtual IEnumerator Cooldown()
    {
        onStartCooldown?.Invoke();

        float timer = 0f;
        float _cooldown = MathF.Max(0.1f, cooldown - cooldown * (_stats.SpeedAttackModifier - 1f));

        while (timer < _cooldown)
        {
            float _cooldownValue = timer / _cooldown;
            updateCooldown?.Invoke(_cooldownValue);

            timer += Time.deltaTime;
            yield return null;
        }

        onStopCooldown?.Invoke();
    }

    protected abstract void SetSubsriptions();
    protected abstract void RemoveSubsriptions();

    public float RangeAttack() => rangeAttack;

    public Sprite GetIcon() => icon;

    protected virtual void OnDestroy()
    {
        RemoveSubsriptions();
    }
}