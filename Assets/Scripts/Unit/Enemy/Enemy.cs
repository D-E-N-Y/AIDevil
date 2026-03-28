using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IUnit
{
    [SerializeField] protected EnemyType _type;
    public EnemyType Type => _type;
    
    [SerializeField] protected string _name;
    [SerializeField] protected EnemyStats _stats;

    protected UnitHealth _health;
    public event Action<IUnit> OnDead;

    protected bool _isDead;
    public bool IsDead => _isDead;

    [SerializeField] UI_HPIndicator ui_hpIndicator;

    [SerializeField] protected PlayerCharacter playerCharacterTarget;

    [SerializeField] protected List<Spell> spells;
    [SerializeField] protected UI_WorldSpellCooldown ui_worldSpellCooldown;

    [SerializeField, Range(1f, 20f)] protected float attackRange;

    [SerializeField] protected WorldResource worldResource;

    protected UnitFaction _unitFaction;

    public virtual void Initialize()
    {
        _unitFaction = UnitFaction.Enemy;
        gameObject.layer = LayerMask.NameToLayer(_unitFaction.ToString());

        _health = new UnitHealth(_stats);
        _health.OnDead += Death;

        ui_hpIndicator.Initialize(_health);

        spells.ForEach(x => x.Initialize(_unitFaction, _stats));
        ui_worldSpellCooldown.Initialize(spells[0]);

        _isDead = false;
        gameObject.SetActive(true);
    }

    protected virtual void Attacking()
    {
        Spell _spells = spells[UnityEngine.Random.Range(0, spells.Count)];
        _spells.Cast();
    } 

    public virtual void Death()
    {
        GameInstance.current.ProfileManager.CurrentProfile.BestiaryProgress.AddEnemy(_name);
        
        DropMoney();

        _isDead = true;

        gameObject.SetActive(false);
        OnDead?.Invoke(this);
    }

    protected void DropMoney()
    {
        WorldResource _worldResource = Instantiate(worldResource, transform.position, Quaternion.identity);
        _worldResource.Initialize(ResourceType.Credits, _stats.DropMoney);
    }

    public virtual void SetPlayerTarget(PlayerCharacter playerTarget) => this.playerCharacterTarget = playerTarget;

    public string GetName() => _name;
    public UnitStats GetStats() => _stats;
    public UnitHealth GetHealth() => _health;

    public List<Spell> GetSpells() => spells;
}