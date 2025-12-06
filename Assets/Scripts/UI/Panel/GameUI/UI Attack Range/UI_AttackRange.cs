using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_AttackRange : UI_Panel 
{
    [SerializeField] private Image ui_spellIcon;
    [SerializeField] private Image ui_spellCooldown;
    
    private SpellRange _spell;

    public void Initialize(SpellRange spell)
    {
        if(_spell != null) ClearSubscriptions();
        
        _spell = spell;
        ui_spellIcon.sprite = _spell.GetIcon();
        ui_spellCooldown.sprite = _spell.GetIcon();;

        AddSubscriptions();

        UpdateCooldown(1f);
    }

    protected override void AddSubscriptions()
    {
        base.AddSubscriptions();

        _spell.updateCooldown += UpdateCooldown;
    }

    protected override void ClearSubscriptions()
    {
        base.ClearSubscriptions();

        _spell.updateCooldown -= UpdateCooldown;
    }

    private void UpdateCooldown(float value)
    {
        ui_spellCooldown.fillAmount = 1f - value;
    }
}