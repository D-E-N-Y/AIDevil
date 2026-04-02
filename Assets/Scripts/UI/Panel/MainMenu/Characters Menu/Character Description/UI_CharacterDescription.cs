using TMPro;
using UnityEngine;

public class UI_CharacterDescription : UI_Panel 
{
    [Header("Panels")]
    [SerializeField] private RectTransform _content;
    [SerializeField] private RectTransform _characterInfo;
    [SerializeField] private RectTransform _lockedInfo;

    [Header("Character Info")]
    [Header("Tittle")]
    [SerializeField] private TextMeshProUGUI ui_nameText;
    [SerializeField] private TextMeshProUGUI ui_levelText;

    [Header("Parameters")]
    [SerializeField] private UI_Stats ui_stats;
    [SerializeField] private UI_SpellsList ui_spellsList;

    [Header("Locked Info")]
    [Header("Cost")]
    [SerializeField] private UI_Cost ui_cost;

    ProfileManager _profileManager;

    public void Initialize(ProfileManager profileManager)
    {
        _profileManager = profileManager;
        
        InitCharacterInfo();
        InitLockedInfo();        

        HideContent();
    }

    private void InitCharacterInfo()
    {
        ui_spellsList.Initialize();
        ui_stats.Initialize();
    }

    private void InitLockedInfo()
    {
        ui_cost.Initialize();
    }

    public void SetCharacterInfo(PlayerCharacter playerCharacter, bool isLocked)
    {
        UpdateCharacterInfo(playerCharacter);
        UpdateLockedInfo(playerCharacter, isLocked);
    }

    private void UpdateCharacterInfo(PlayerCharacter playerCharacter)
    {
        ui_nameText.text = playerCharacter.GetName();
        ui_levelText.text = _profileManager.CurrentProfile.CharacterManager.GetCharacterLevel().ToString();

        ui_stats.SetStats((PlayerCharacterStats)playerCharacter.GetStats());
        ui_stats.UpdateUI();

        ui_spellsList.SetInfo(playerCharacter.GetStartItems().GetStartSpells());
    }

    private void UpdateLockedInfo(PlayerCharacter playerCharacter, bool isLocked)
    {
        _lockedInfo.gameObject.SetActive(isLocked);
        if (isLocked)
        {
            ui_cost.UpdateUICost(playerCharacter.Cost);
        }
    }

    public void ShowContent() => _content.gameObject.SetActive(true);
    public void HideContent() => _content.gameObject.SetActive(false);
}