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

    [SerializeField, Range(0.1f, 15f)] protected float cooldown;
    
    [SerializeField] protected Weapon _weapon;
    public Weapon Weapon => _weapon;
    protected Action _weaponHandler;

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

    protected SpellContext _spellContext;

    public virtual void Initialize(SpellContext spellContext)
    {
        RemoveSubsriptions();
        
        attacking = null;
        
        _spellContext = spellContext;

        SetStats();

        _weapon.Initialize(_spellContext.UnitFaction);
        
        SetSubsriptions();
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
        _damageModifier = _spellContext.Stats.DamageModifier;
        _speedAttackModifier = _spellContext.Stats.SpeedAttackModifier;
        _criticalDamageChance = _spellContext.Stats.CriticalDamageChance;
        _criticalDamageModifier = _spellContext.Stats.CriticalDamageModifier;
        _multiattackChance = _spellContext.Stats.MultiattackChance;
        _areaModifier = _spellContext.Stats.AreaModifier;
    }

    public abstract void Cast();
    protected abstract IEnumerator Attacking();
    protected abstract IEnumerator Attack();
    protected virtual IEnumerator Cooldown()
    {
        onStartCooldown?.Invoke();

        float timer = 0f;
        float _cooldown = MathF.Max(0.1f, cooldown - cooldown * (_speedAttackModifier - 1f));

        while (timer < _cooldown)
        {
            float _cooldownValue = timer / _cooldown;
            updateCooldown?.Invoke(_cooldownValue);

            timer += Time.deltaTime;
            yield return null;
        }

        onStopCooldown?.Invoke();
    }

    protected bool IsMultiattack()
    {
        float roll = UnityEngine.Random.Range(0f, 1f);
        return roll < _multiattackChance;
    }

    protected virtual void SetSubsriptions()
    {
        _spellContext.Stats.OnStatChanged += UpdateStats;
        
        _weaponHandler = () => onSuccessfulAttack?.Invoke();
        _weapon.onSuccessfulAttack += _weaponHandler;
    }

    protected virtual void RemoveSubsriptions()
    {
        if (_spellContext != null)
        {
            _spellContext.Stats.OnStatChanged -= UpdateStats;
        }

        if (_weaponHandler != null)
        {
            _weapon.onSuccessfulAttack -= _weaponHandler;
        }
    }

    public float RangeAttack() => rangeAttack;
    public float GetCooldown() => cooldown;

    public Sprite GetIcon() => icon;

    protected virtual void OnDestroy()
    {
        RemoveSubsriptions();
    }
}