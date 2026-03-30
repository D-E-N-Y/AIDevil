using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_CharacterUpgradeMenu : UI_Panel 
{
    [Header("Title")]
    [SerializeField] private TextMeshProUGUI ui_nameCharacterText;
    
    [Header("Buttons")]
    [SerializeField] private Button ui_closeButton;

    [Header("Upgrade Trees")] 
    [SerializeField] private List<UI_UpgradeTree> ui_upgradeTrees;
    private Dictionary<string, UI_UpgradeTree> ui_upgradeTreesChash;
    private UI_UpgradeTree _selectUIUpgradeTree;

    [Header("Description")] 
    [SerializeField] private UI_UpgradeDescription ui_upgradeDescription;

    [Header("Wallet")]
    [SerializeField] private UI_Wallet ui_wallet;

    [Header("Purchase Section")]
    [SerializeField] private RectTransform _purchasePanel;
    [SerializeField] private Button ui_purchaseButton;

    private PurchaseUpgrade _purchaseUpgrade;
    private GameInstance _gameInstance;

    public void Initialize(GameInstance gameInstance)
    {
        _gameInstance = gameInstance;
        _purchaseUpgrade = new PurchaseUpgrade(_gameInstance.ProfileManager, _gameInstance.DataBase.UpgradeTrees);

        ui_upgradeDescription.Initialize();

        ui_wallet.Initialize(_gameInstance.ProfileManager.CurrentProfile.Wallet);

        ui_closeButton.onClick.RemoveAllListeners();
        ui_closeButton.onClick.AddListener(() => Hide());

        CashUIUpgradeTree();

        AddSubscriptions();
    }

    private void CashUIUpgradeTree()
    {
        ui_upgradeTreesChash = new Dictionary<string, UI_UpgradeTree>();
        foreach (UI_UpgradeTree ui_upgradeTree in ui_upgradeTrees)
        {
            ui_upgradeTreesChash[ui_upgradeTree.UpgradeTree.PlayerCharacter.ID] = ui_upgradeTree;
            ui_upgradeTree.Initialize(_gameInstance.ProfileManager.CurrentProfile.CharacterManager.UpgradeContainer);
            ui_upgradeTree.Hide();
        }
    }

    private void RefreshProfileUI()
    {
        ui_wallet.UpdateWallet(_gameInstance.ProfileManager.CurrentProfile.Wallet);
    }

    private void UpdateTree()
    {
        string characterName = _gameInstance.DataBase.Characters.GetCharacterByID(_gameInstance.ProfileManager.CurrentProfile.CharacterManager.Character_ID).GetName();
        ui_nameCharacterText.text = characterName;
        
        if (_selectUIUpgradeTree != null)
        {
            _selectUIUpgradeTree.Hide();
            _selectUIUpgradeTree = null;
        }

        if (ui_upgradeTreesChash.ContainsKey(_gameInstance.ProfileManager.CurrentProfile.CharacterManager.Character_ID))
        {
            _selectUIUpgradeTree = ui_upgradeTreesChash[_gameInstance.ProfileManager.CurrentProfile.CharacterManager.Character_ID];
            _selectUIUpgradeTree.UpdateTree(_gameInstance.ProfileManager.CurrentProfile.CharacterManager.UpgradeContainer);
            _selectUIUpgradeTree.UnSelectUpgrade();
            _selectUIUpgradeTree.Show();

            ui_upgradeDescription.HideContent();
        }
        else
        {
            Debug.LogWarning($"Upgrade Tree for {_gameInstance.ProfileManager.CurrentProfile.CharacterManager.Character_ID} not found!!!");
        }

        _purchasePanel.gameObject.SetActive(false);
        DisablePurchaseButton();
    }

    private void UpdatePurchaseButton(string upgrade_id)
    {
        ui_purchaseButton.onClick.RemoveAllListeners();
        ui_purchaseButton.interactable = _purchaseUpgrade.CanPurchase(upgrade_id);

        ui_purchaseButton.onClick.AddListener(() => _purchaseUpgrade.Purchase(upgrade_id));
    }

    private void DisablePurchaseButton()
    {
        ui_purchaseButton.onClick.RemoveAllListeners();
        ui_purchaseButton.interactable = false;
    }

    private void SelectUpgrade(string upgrade_id)
    {
        ui_upgradeDescription.ShowContent();
        ui_upgradeDescription.SetInfo(_selectUIUpgradeTree.UpgradeTree.GetUpgradeByID(upgrade_id));
        
        _purchasePanel.gameObject.SetActive(true);
        UpdatePurchaseButton(upgrade_id);
    }

    protected override void AddSubscriptions()
    {
        base.AddSubscriptions();

        _purchaseUpgrade.onPurchase += UpdateTree;
        foreach (UI_UpgradeTree ui_upgradeTree in ui_upgradeTrees)
        {
            ui_upgradeTree.OnSelectUpgrade += SelectUpgrade;
        }

        _gameInstance.ProfileManager.onCurrentProfileChanged += RefreshProfileUI;
    }

    protected override void ClearSubscriptions()
    {
        base.ClearSubscriptions();

        _purchaseUpgrade.onPurchase -= UpdateTree;
        foreach (UI_UpgradeTree ui_upgradeTree in ui_upgradeTrees)
        {
            ui_upgradeTree.OnSelectUpgrade -= SelectUpgrade;
        }

        _gameInstance.ProfileManager.onCurrentProfileChanged -= RefreshProfileUI;
    }

    public override void Show()
    {
        UpdateTree();
        
        base.Show();
    }
}