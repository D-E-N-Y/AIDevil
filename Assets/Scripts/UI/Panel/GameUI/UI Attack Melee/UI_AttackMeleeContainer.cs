using System.Collections.Generic;
using UnityEngine;

public class UI_AttackMeleeContainer : UI_Panel
{
    [SerializeField] private List<UI_AttackMelee> ui_spells;

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

        List<SpellMelee> _spellMelles = _playerCharacter.GetSpellMelees();

        for (int i = 0; i < _spellMelles.Count; i++)
        {
            ui_spells[i].Initialize(_spellMelles[i]);
            ui_spells[i].Show();
        }
    }
}
