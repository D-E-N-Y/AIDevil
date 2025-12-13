using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class Spell : MonoBehaviour
{
    public Action<float> updateCooldown;
    public Action startCooldown;
    public Action stopCooldown;

    public Action onSuccessfulAttack;
    public bool IsAttacking { get; protected set; }

    [SerializeField, Range(0.1f, 100f)] protected float rangeAttack;

    protected UnitFaction _unitFaction;
    [SerializeField, Range(0.1f, 15f)] protected float cooldown;
    protected Coroutine attacking;

    [SerializeField] private UI_SpellCooldown ui_SpellCooldown;

    [SerializeField] private Sprite icon;

    public virtual void Initialize(UnitFaction unitFaction)
    {
        attacking = null;
        _unitFaction = unitFaction;

        SetUISpellCooldown(ui_SpellCooldown);
    }

    public void SetUISpellCooldown(UI_SpellCooldown ui_SpellCooldown)
    {
        if (ui_SpellCooldown != null)
        {
            ui_SpellCooldown.Initialize(this);
            this.ui_SpellCooldown = ui_SpellCooldown;
        }
    }

    public abstract void Cast();
    protected abstract IEnumerator Attacking();
    protected abstract IEnumerator Attack();
    protected virtual IEnumerator Cooldown()
    {
        startCooldown?.Invoke();

        float timer = 0f;
        while (timer < cooldown)
        {
            float _cooldownValue = timer / cooldown;
            updateCooldown?.Invoke(_cooldownValue);

            timer += Time.deltaTime;
            yield return null;
        }

        stopCooldown?.Invoke();
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