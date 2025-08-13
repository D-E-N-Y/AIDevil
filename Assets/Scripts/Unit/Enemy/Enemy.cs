using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IHealth
{
    [SerializeField, Range(1, 1000)] protected int maxHP;
    protected int currentHP;

    [SerializeField] UI_UnitHPIndicator ui_unitHPIndicator;

    [SerializeField, Range(1f, 100f)] protected float moveSpeed;

    [SerializeField] protected Player playerTarget;

    public Action<Enemy> onDead;

    public virtual void Initialize()
    {
        currentHP = maxHP;

        ui_unitHPIndicator.Initialize(this);

        gameObject.SetActive(true);
    }

    public virtual void TakeDamage(int value)
    {
        value = Math.Max(0, value);
        currentHP -= value;

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