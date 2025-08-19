using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IHealth
{
    public Action<IHealth> onDead { get; set; }
    public Action onChangeHP { get; set; }

    [SerializeField, Range(1, 1000)] protected int maxHP;
    protected int currentHP;

    [SerializeField] UI_UnitHPIndicator ui_unitHPIndicator;

    [SerializeField, Range(1f, 100f)] protected float moveSpeed;

    [SerializeField] protected Player playerTarget;

    [SerializeField] protected List<Spell> spells;

    protected string _originLayer;

    public virtual void Initialize()
    {
        _originLayer = "Enemy";
        gameObject.layer = LayerMask.NameToLayer(_originLayer);

        currentHP = maxHP;

        ui_unitHPIndicator.Initialize(this);

        spells.ForEach(x => x.Initialize(_originLayer));

        gameObject.SetActive(true);
    }

    public virtual void TakeDamage(int value)
    {
        value = Math.Max(0, value);
        currentHP -= value;

        onChangeHP?.Invoke();

        if (currentHP <= 0)
        {
            Death();
        }
    }

    public virtual void Death()
    {
        gameObject.SetActive(false);
        onDead?.Invoke(this);
    }

    public virtual void SetPlayerTarget(Player playerTarget) => this.playerTarget = playerTarget;
    
    public int GetCurrentHP() => currentHP;
    public int GetMaxHP() => maxHP;
}