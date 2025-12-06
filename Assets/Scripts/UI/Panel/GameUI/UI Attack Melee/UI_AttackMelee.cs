using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_AttackMelee : UI_Panel 
{
    [SerializeField] private Image ui_spellIcon;
    [SerializeField] private Image ui_spellCooldown;
    
    [SerializeField] private Button ui_button;
    
    private SpellMelee _spell; 

    public void Initialize(SpellMelee spell)
    {
        if(_spell != null) ClearSubscriptions();
        
        _spell = spell;
        ui_spellIcon.sprite = _spell.GetIcon();
        ui_spellCooldown.sprite = _spell.GetIcon();;

        SetSpellContoler();
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

    private void SetSpellContoler()
    {
        ui_button.onClick.RemoveAllListeners();
        ui_button.onClick.AddListener(() => _spell.Cast());
    }
}