using UnityEngine;
using UnityEngine.UI;

public abstract class UI_WorldSpellCooldown : UI_Panel
{
    [SerializeField] protected Transform canvasTransform;
    
    [SerializeField] protected RectTransform _rectTransform;
    [SerializeField] protected Image ui_cooldownImage;
    protected Spell _spell;

    public virtual void Initialize(Spell spell)
    {
        Hide();

        _spell = spell;

        ClearSubscriptions();
        AddSubscriptions();
    }

    protected override void AddSubscriptions()
    {
        _spell.updateCooldown += SetCooldown;
        _spell.onStartCooldown += Show;
        _spell.onStopCooldown += Hide;
    }

    protected override void ClearSubscriptions()
    {
        _spell.updateCooldown -= SetCooldown;
        _spell.onStartCooldown -= Show;
        _spell.onStopCooldown -= Hide;
    }

    public abstract void SetCooldown(float value);
}