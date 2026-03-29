using UnityEngine;
using UnityEngine.UI;

public class UI_CharactersMenu : UI_Panel
{
    [Header("Panels")]
    [SerializeField] private UI_CharactersList ui_charactersList;
    [SerializeField] private UI_CharacterDescription ui_characterDescription;

    [Header("Wallet")]
    [SerializeField] private UI_Wallet ui_wallet;

    [Header("Buttons")]
    [SerializeField] private Button ui_closeButton;

    [Header("Purchase Section")]
    [SerializeField] private RectTransform _purcgasePanel;
    [SerializeField] private Button ui_purchaseButton;

    [Header("Character Control Section")]
    [SerializeField] private RectTransform _characterControlPanel;
    [SerializeField] private Button ui_chooseGameLevelButton;
    [SerializeField] private Button ui_upgradeButton;

    private GameInstance _gameInstance;

    public void Initialize(GameInstance gameInstance, UI_GameLevelsMenu ui_gameLevelsMenu, UI_CharacterUpgradeMenu ui_characterUpgradeMenu)
    {
        _gameInstance = gameInstance;
        
        ui_characterDescription.Initialize(_gameInstance.ProfileManager);
        ui_charactersList.Initialize(gameInstance, this);

        ui_wallet.Initialize(_gameInstance.ProfileManager.CurrentProfile.Wallet);

        ui_closeButton.onClick.RemoveAllListeners();
        ui_closeButton.onClick.AddListener(() => Hide()); 
        
        ui_chooseGameLevelButton.onClick.RemoveAllListeners();
        ui_chooseGameLevelButton.onClick.AddListener(() => ui_gameLevelsMenu.Show());

        ui_upgradeButton.onClick.RemoveAllListeners();
        ui_upgradeButton.onClick.AddListener(() => ui_characterUpgradeMenu.Show());
    }

    public void Select(PlayerCharacter character)
    {
        _gameInstance.ProfileManager.CurrentProfile.CharacterManager.SetCharacter(character.ID);
        
        ui_characterDescription.SetCharacterInfo(character);
        ui_characterDescription.ShowContent();
    }
}
