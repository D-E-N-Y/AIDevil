using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UI_SpellsList : UI_Panel 
{
    [SerializeField] private UI_Spell ui_spellPrefab;
    [SerializeField] private Transform ui_spellsContainer;

    public void SetInfo(List<Spell> spells)
    {
        List<UI_Spell> ui_spells = new List<UI_Spell>();
        ui_spells = ui_spellsContainer.GetComponentsInChildren<UI_Spell>(true).ToList();

        int residue = Mathf.Abs(ui_spells.Count - spells.Count);
        if(residue > 0)
        {
            for(int i = 0; i < residue; i++)
            {
                UI_Spell _ui_spell = Instantiate(ui_spellPrefab, ui_spellsContainer);
                ui_spells.Add(_ui_spell);
            }
        }

        ui_spells.ForEach(s => s.Hide());

        for(int i = 0; i < spells.Count; i++)
        {
            ui_spells[i].Initialize(spells[i]);
            ui_spells[i].Show();
        }
    }
}