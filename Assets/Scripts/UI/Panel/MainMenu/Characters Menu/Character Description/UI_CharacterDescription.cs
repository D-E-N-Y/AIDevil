using TMPro;
using UnityEngine;

public class UI_CharacterDescription : UI_Panel 
{
    [SerializeField] private TextMeshProUGUI ui_nameText;
    [SerializeField] private UI_Stats ui_stats;
    [SerializeField] private UI_SpellsList ui_spellsList;

    public void Initialize()
    {
        ui_spellsList.Initialize();
        ui_stats.Initialize();
    }

    public void SetCharacterInfo(PlayerCharacter playerCharacter)
    {
        ui_nameText.text = playerCharacter.GetName();

        ui_stats.SetStats((PlayerCharacterStats)playerCharacter.GetStats());
        ui_stats.UpdateUI();

        ui_spellsList.SetInfo(playerCharacter.GetStartItems().GetStartSpells());
    }
}