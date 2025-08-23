using UnityEngine;
using UnityEngine.UI;

public abstract class UI_SpellCooldown : UI_Panel
{
    [SerializeField] protected RectTransform _rectTransform;
    [SerializeField] protected Image ui_cooldownImage;
    protected Spell _spell;

    public virtual void Initialize(Spell spell)
    {
        Hide();

        _spell = spell;

        RemoveSubsriptions();
        SetSubsriptions();
    }

    protected void SetSubsriptions()
    {
        _spell.updateCooldown += SetCooldown;
        _spell.startCooldown += Show;
        _spell.stopCooldown += Hide;
    }

    protected void RemoveSubsriptions()
    {
        _spell.updateCooldown -= SetCooldown;
        _spell.startCooldown -= Show;
        _spell.stopCooldown -= Hide;
    }

    public abstract void SetCooldown(float value);
}