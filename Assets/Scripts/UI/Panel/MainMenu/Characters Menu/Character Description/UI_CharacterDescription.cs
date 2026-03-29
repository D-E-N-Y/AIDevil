using TMPro;
using UnityEngine;

public class UI_CharacterDescription : UI_Panel 
{
    [Header("Tittle")]
    [SerializeField] private TextMeshProUGUI ui_nameText;
    [SerializeField] private TextMeshProUGUI ui_levelText;

    [Header("Parameters")]
    [SerializeField] private UI_Stats ui_stats;
    [SerializeField] private UI_SpellsList ui_spellsList;

    [Header("Content")]
    [SerializeField] private RectTransform _content;

    ProfileManager _profileManager;

    public void Initialize(ProfileManager profileManager)
    {
        _profileManager = profileManager;
        
        ui_spellsList.Initialize();
        ui_stats.Initialize();

        HideContent();
    }

    public void SetCharacterInfo(PlayerCharacter playerCharacter)
    {
        ui_nameText.text = playerCharacter.GetName();
        ui_levelText.text = _profileManager.CurrentProfile.CharacterManager.GetCharacterLevel().ToString();

        ui_stats.SetStats((PlayerCharacterStats)playerCharacter.GetStats());
        ui_stats.UpdateUI();

        ui_spellsList.SetInfo(playerCharacter.GetStartItems().GetStartSpells());
    }

    public void ShowContent() => _content.gameObject.SetActive(true);
    public void HideContent() => _content.gameObject.SetActive(false);
}