using System.Collections.Generic;
using UnityEngine;

public class UI_AttackRangeContainer : UI_Panel
{
    [SerializeField] private List<UI_AttackRange> ui_spells;

    private Player _playerCharacter;

    public void Initialize(Player playerCharacter)
    {
        _playerCharacter = playerCharacter;
        _playerCharacter.updateSpells += UpdateUI;

        UpdateUI();
    }

    private void UpdateUI()
    {
        ui_spells.ForEach(x => x.Hide()); 

        List<SpellRange> _spellRanges = _playerCharacter.GetSpellRanges();

        for (int i = 0; i < _spellRanges.Count; i++)
        {
            ui_spells[i].Initialize(_spellRanges[i]);
            ui_spells[i].Show();
        }
    }
}
