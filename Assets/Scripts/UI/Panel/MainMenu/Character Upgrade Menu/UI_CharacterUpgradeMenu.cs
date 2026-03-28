using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CharacterUpgradeMenu : UI_Panel 
{
    [SerializeField] private Button ui_closeButton;

    [SerializeField] private List<UI_UpgradeTree> ui_upgradeTrees;
    private Dictionary<string, UI_UpgradeTree> ui_upgradeTreesChash;
    private UI_UpgradeTree _selectUIUpgradeTree;

    [SerializeField] private UI_UpgradeDescription ui_upgradeDescription;

    private GameInstance _gameInstance;

    public void Initialize(GameInstance gameInstance)
    {
        _gameInstance = gameInstance;

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
            ui_upgradeTree.Initialize(_gameInstance.UpgradeContainer);
            ui_upgradeTree.Hide();
        }
    }

    private void SelectUpgradeTree()
    {
        if (_selectUIUpgradeTree != null)
        {
            _selectUIUpgradeTree.Hide();
            _selectUIUpgradeTree = null;
        }

        if (ui_upgradeTreesChash.ContainsKey(_gameInstance.GetProfile().PlayerCharacter_ID))
        {
            _selectUIUpgradeTree = ui_upgradeTreesChash[_gameInstance.GetProfile().PlayerCharacter_ID];
            _selectUIUpgradeTree.UpdateTree(_gameInstance.UpgradeContainer);
            _selectUIUpgradeTree.Show();
        }
        else
        {
            Debug.LogWarning($"Upgrade Tree for {_gameInstance.GetProfile().PlayerCharacter_ID} not found!!!");
        }
    }

    private void SelectUpgrade(string upgrade_id)
    {
        ui_upgradeDescription.SetInfo(_selectUIUpgradeTree.UpgradeTree.GetUpgradeByID(upgrade_id));
    }

    protected override void AddSubscriptions()
    {
        base.AddSubscriptions();

        foreach (UI_UpgradeTree ui_upgradeTree in ui_upgradeTrees)
        {
            ui_upgradeTree.OnSelectUpgrade += SelectUpgrade;
        }

        _gameInstance.OnChangePlayerCharacter += SelectUpgradeTree;
    }

    protected override void ClearSubscriptions()
    {
        base.ClearSubscriptions();

        foreach (UI_UpgradeTree ui_upgradeTree in ui_upgradeTrees)
        {
            ui_upgradeTree.OnSelectUpgrade -= SelectUpgrade;
        }

        _gameInstance.OnChangePlayerCharacter -= SelectUpgradeTree;
    }
}