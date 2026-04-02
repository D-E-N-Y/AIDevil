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

    private PurchaseCharacter _purchaseCharacter;
    private GameInstance _gameInstance;

    public void Initialize(GameInstance gameInstance, UI_GameLevelsMenu ui_gameLevelsMenu, UI_CharacterUpgradeMenu ui_characterUpgradeMenu)
    {
        ClearSubscriptions();
        
        _gameInstance = gameInstance;
        _purchaseCharacter = new PurchaseCharacter(_gameInstance.ProfileManager, _gameInstance.DataBase.Characters);
        
        ui_characterDescription.Initialize(_gameInstance.ProfileManager);
        ui_charactersList.Initialize(gameInstance, this);

        ui_wallet.Initialize(_gameInstance.ProfileManager.CurrentProfile.Wallet);

        _purcgasePanel.gameObject.SetActive(false);
        _characterControlPanel.gameObject.SetActive(false);

        ui_closeButton.onClick.RemoveAllListeners();
        ui_closeButton.onClick.AddListener(() => Hide()); 
        
        ui_chooseGameLevelButton.onClick.RemoveAllListeners();
        ui_chooseGameLevelButton.onClick.AddListener(() => ui_gameLevelsMenu.Show());

        ui_upgradeButton.onClick.RemoveAllListeners();
        ui_upgradeButton.onClick.AddListener(() => ui_characterUpgradeMenu.Show());

        AddSubscriptions();
    }

    public void Select(PlayerCharacter character)
    {
        _gameInstance.ProfileManager.CurrentProfile.CharacterManager.SetCharacter(character.ID);

        bool isCharacterUnlocked = _gameInstance.ProfileManager.CurrentProfile.CharacterManager.CharacterProgress.IsCharacterUnlocked(character.ID);
        
        ui_characterDescription.SetCharacterInfo(character, !isCharacterUnlocked);
        ui_characterDescription.ShowContent();

        UpdatePurchaseSection(!isCharacterUnlocked, character.ID);
        UpdateCharacterControlSection(isCharacterUnlocked);
    }

    private void UpdateData()
    {
        PlayerCharacter character = _gameInstance.DataBase.Characters.GetCharacterByID(_gameInstance.ProfileManager.CurrentProfile.CharacterManager.Character_ID);
        string characterID = character.ID;
        bool isCharacterUnlocked = _gameInstance.ProfileManager.CurrentProfile.CharacterManager.CharacterProgress.IsCharacterUnlocked(characterID);

        UpdatePurchaseSection(!isCharacterUnlocked, characterID);
        UpdateCharacterControlSection(isCharacterUnlocked);

        ui_characterDescription.SetCharacterInfo(character, !isCharacterUnlocked);
    }

    private void RefreshProfileUI()
    {
        ui_wallet.UpdateWallet(_gameInstance.ProfileManager.CurrentProfile.Wallet);

        UpdatePurchaseSection(false, null);
        UpdateCharacterControlSection(false);

        ui_characterDescription.HideContent();
    }

    private void UpdatePurchaseSection(bool isAvailable, string characterID)
    {
        _purcgasePanel.gameObject.SetActive(isAvailable);
        
        if (!isAvailable) return;

        ui_purchaseButton.onClick.RemoveAllListeners();
        ui_purchaseButton.onClick.AddListener( () =>
        {
            _purchaseCharacter.Purchase(characterID);
        });

        ui_purchaseButton.interactable = _purchaseCharacter.CanPurchase(characterID);
    }

    private void UpdateCharacterControlSection(bool isAvailable)
    {
        _characterControlPanel.gameObject.SetActive(isAvailable);
    }

    protected override void AddSubscriptions()
    {
        base.AddSubscriptions();

        _purchaseCharacter.onPurchaseComplete += UpdateData;
        _gameInstance.ProfileManager.onCurrentProfileChanged += RefreshProfileUI;
    }

    protected override void ClearSubscriptions()
    {
        base.ClearSubscriptions();

        if (_purchaseCharacter != null)
            _purchaseCharacter.onPurchaseComplete -= UpdateData;

        if (_gameInstance != null && _gameInstance.ProfileManager != null)
            _gameInstance.ProfileManager.onCurrentProfileChanged -= RefreshProfileUI;
    }
}
