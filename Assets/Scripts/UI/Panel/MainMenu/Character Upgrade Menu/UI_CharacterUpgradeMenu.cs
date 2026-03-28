using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CharacterUpgradeMenu : UI_Panel 
{
    [Header("Buttons")]
    [SerializeField] private Button ui_closeButton;
    [SerializeField] private Button ui_purchaseButton;

    [Header("Upgrade Trees")] 
    [SerializeField] private List<UI_UpgradeTree> ui_upgradeTrees;
    private Dictionary<string, UI_UpgradeTree> ui_upgradeTreesChash;
    private UI_UpgradeTree _selectUIUpgradeTree;

    [Header("Description")] 
    [SerializeField] private UI_UpgradeDescription ui_upgradeDescription;

    private PurchaseUpgrade _purchaseUpgrade;
    private GameInstance _gameInstance;

    public void Initialize(GameInstance gameInstance)
    {
        _gameInstance = gameInstance;
        _purchaseUpgrade = new PurchaseUpgrade(_gameInstance.ProfileManager, _gameInstance.DataBase.UpgradeTrees);

        ui_upgradeDescription.Initialize();

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

    private void UpdateTree()
    {
        if (_selectUIUpgradeTree != null)
        {
            _selectUIUpgradeTree.Hide();
            _selectUIUpgradeTree = null;
        }

        if (ui_upgradeTreesChash.ContainsKey(_gameInstance.ProfileManager.CurrentProfile.CharacterManager.Character_ID))
        {
            _selectUIUpgradeTree = ui_upgradeTreesChash[_gameInstance.ProfileManager.CurrentProfile.CharacterManager.Character_ID];
            _selectUIUpgradeTree.UpdateTree(_gameInstance.ProfileManager.CurrentProfile.CharacterManager.UpgradeContainer);
            _selectUIUpgradeTree.Show();
        }
        else
        {
            Debug.LogWarning($"Upgrade Tree for {_gameInstance.ProfileManager.CurrentProfile.CharacterManager.Character_ID} not found!!!");
        }

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
        ui_upgradeDescription.SetInfo(_selectUIUpgradeTree.UpgradeTree.GetUpgradeByID(upgrade_id));
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
    }

    protected override void ClearSubscriptions()
    {
        base.ClearSubscriptions();

        _purchaseUpgrade.onPurchase -= UpdateTree;
        foreach (UI_UpgradeTree ui_upgradeTree in ui_upgradeTrees)
        {
            ui_upgradeTree.OnSelectUpgrade -= SelectUpgrade;
        }
    }

    public override void Show()
    {
        UpdateTree();
        
        base.Show();
    }
}