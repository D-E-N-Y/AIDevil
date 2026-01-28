using TMPro;
using UnityEngine;

public class UI_CharacterDescription : UI_Panel 
{
    [SerializeField] private TextMeshProUGUI ui_nameText;
    [SerializeField] private TextMeshProUGUI ui_hpText;
    [SerializeField] private TextMeshProUGUI ui_armorText;
    [SerializeField] private TextMeshProUGUI ui_speedText;
    [SerializeField] private UI_SpellsList ui_spellsList;

    public void SetCharacterInfo(PlayerCharacter playerCharacter)
    {
        ui_nameText.text = playerCharacter.GetName();
        ui_hpText.text = playerCharacter.GetStats().MaxHP.ToString();
        ui_armorText.text = playerCharacter.GetStats().Armor.ToString();
        ui_speedText.text = playerCharacter.GetStats().BaseMoveSpeed.ToString();

        // ui_spellsList.SetInfo(playerCharacter.GetSpellController().GetSpells());
        ui_spellsList.SetInfo(playerCharacter.GetStartItems().GetStartSpells());
    }
}