using UnityEngine;
using UnityEngine.UI;

public class UI_Spell : UI_Panel 
{
    [SerializeField] private Image ui_spellImage;

    private Spell _spell;

    public void Initialize(Spell spell)
    {
        _spell = spell;
        ui_spellImage.sprite = _spell.GetIcon();
    }

    public Spell GetSpell() => _spell;
}