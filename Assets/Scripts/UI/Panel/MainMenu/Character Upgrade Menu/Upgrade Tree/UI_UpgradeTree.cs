using System;
using System.Collections.Generic;
using UnityEngine;

public class UI_UpgradeTree : UI_Panel 
{
    public event Action<string> OnSelectUpgrade;

    [SerializeField] private UpgradeTree _upgradeTree;
    public UpgradeTree UpgradeTree => _upgradeTree;

    [SerializeField] private List<UI_Upgrade> ui_upgrades;
    private UI_Upgrade _selectUIUpgrade;

    public void Initialize(UpgradeContainer upgradeContainer)
    {
        foreach (UI_Upgrade ui_upgrade in ui_upgrades)
        {
            ui_upgrade.Initialize();
            ui_upgrade.OnSelect += SelectUpgrade;

            ui_upgrade.SetStatus(
                HasRequiredUpgrades(ui_upgrade.Upgrade, upgradeContainer),
                false
            );
        }
    }

    private bool HasRequiredUpgrades(Upgrade upgrade, UpgradeContainer upgradeContainer)
    {
        bool result = true;

        foreach (Upgrade requireComponent in upgrade.RequiredUpgrades)
        {
            if (requireComponent == null)
            {
                result = true;
            }
            else
            {
                result = result && upgradeContainer.HasUpgrade(requireComponent);
            }
        }

        return !result;
    }

    public void UpdateTree(UpgradeContainer upgradeContainer)
    {
        foreach (UI_Upgrade ui_upgrade in ui_upgrades)
        {
            ui_upgrade.SetStatus(
                HasRequiredUpgrades(ui_upgrade.Upgrade, upgradeContainer),
                upgradeContainer.HasUpgrade(ui_upgrade.Upgrade)
            );
        }
    }

    public void SelectUpgrade(UI_Upgrade ui_upgrade)
    {
        if (_selectUIUpgrade == ui_upgrade) return;

        UnSelectUpgrade();

        _selectUIUpgrade = ui_upgrade;

        OnSelectUpgrade?.Invoke(_selectUIUpgrade.Upgrade_ID);
    }

    public void UnSelectUpgrade()
    {
        if (_selectUIUpgrade != null)
        {
            _selectUIUpgrade.UnSelect();
        }

        _selectUIUpgrade = null;
    }
}