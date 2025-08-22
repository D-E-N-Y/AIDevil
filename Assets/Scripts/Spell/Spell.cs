using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    public Action onSuccessfulAttack;
    public bool IsAttacking { get; protected set; }

    protected UnitFaction _unitFaction;
    [SerializeField, Range(0.1f, 15f)] protected float cooldown;
    protected Coroutine attacking;

    public abstract void Initialize(UnitFaction unitFaction);
    public abstract void Cast();

    protected abstract IEnumerator Attacking();
    protected abstract IEnumerator Attack();
    protected virtual IEnumerator Cooldown()
    {
        float timer = 0f;
        while (timer < cooldown)
        {
            timer += Time.deltaTime;
            yield return null;
        }
    }

    protected abstract void SetSubsriptions();
    protected abstract void RemoveSubsriptions();
}