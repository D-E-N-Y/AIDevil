using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IUnit
{
    [SerializeField] protected string _name;
    [SerializeField] protected EnemyStats _stats;

    protected UnitHealth _health;
    public event Action<IUnit> OnDead;

    [SerializeField] UI_UnitHPIndicator ui_unitHPIndicator;

    [SerializeField] protected PlayerCharacter playerCharacterTarget;

    [SerializeField] protected List<Spell> spells;
    [SerializeField] protected UI_WorldSpellCooldown ui_worldSpellCooldown;

    [SerializeField, Range(1f, 20f)] protected float attackRange;

    [SerializeField] protected WorldMoney worldMoney;

    protected UnitFaction _unitFaction;

    public virtual void Initialize()
    {
        _unitFaction = UnitFaction.Enemy;
        gameObject.layer = LayerMask.NameToLayer(_unitFaction.ToString());

        _health = new UnitHealth(_stats);
        _health.OnDead += Death;

        ui_unitHPIndicator.Initialize(_health);

        spells.ForEach(x => x.Initialize(_unitFaction, _stats));
        ui_worldSpellCooldown.Initialize(spells[0]);

        gameObject.SetActive(true);
    }

    protected virtual void Attacking()
    {
        Spell _spells = spells[UnityEngine.Random.Range(0, spells.Count)];
        _spells.Cast();
    } 

    public virtual void Death()
    {
        GameInstance.current.GetProfile().bestiaryData.AddDiscoveredEnemy(_name);
        
        DropMoney();

        gameObject.SetActive(false);
        OnDead?.Invoke(this);
    }

    protected void DropMoney()
    {
        WorldMoney _worldMoney = Instantiate(worldMoney, transform.position, Quaternion.identity);
        _worldMoney.Initialize(_stats.DropMoney);
    }

    public virtual void SetPlayerTarget(PlayerCharacter playerTarget) => this.playerCharacterTarget = playerTarget;

    public string GetName() => _name;
    public UnitStats GetStats() => _stats;
    public UnitHealth GetHealth() => _health;

    public List<Spell> GetSpells() => spells;
}