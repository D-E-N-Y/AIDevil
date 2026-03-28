using System;
using System.Collections.Generic;

public class PurchaseUpgrade 
{
    public event Action onPurchase;

    private ProfileManager _profileManager;
    private DB_UpgradeTrees _db_upgradeTrees;

    public PurchaseUpgrade(ProfileManager profileManager, DB_UpgradeTrees db_upgradeTrees)
    {
        _profileManager = profileManager;
        _db_upgradeTrees = db_upgradeTrees;
    }

    public bool CanPurchase(Upgrade upgrade)
    {
        if (IsHasUpgrade(upgrade)) return false;
        if (!IsHasEhoughResources(upgrade.Cost)) return false;
        if (!IsHasRequiredUpgrades(upgrade.RequiredUpgrades)) return false;

        return true;
    }

    private bool IsHasUpgrade(Upgrade upgrade)
    {
        return _profileManager.CurrentProfile.CharacterManager.UpgradeContainer.HasUpgrade(upgrade);
    }

    private bool IsHasEhoughResources(IReadOnlyList<Cost> costs)
    {
        foreach (Cost cost in costs)
        {
            if (!_profileManager.CurrentProfile.Wallet.HasEnoughResource(cost.resource, cost.amount))
            {
                return false;
            }
        }

        return true;
    }

    private bool IsHasRequiredUpgrades(IReadOnlyList<Upgrade> requiredUpgrades)
    {
        foreach (Upgrade upgrade in requiredUpgrades)
        {
            if (!_profileManager.CurrentProfile.CharacterManager.UpgradeContainer.HasUpgrade(upgrade))
            {
                return false;
            }
        }

        return true;
    }

    public bool CanPurchase(string upgradeID)
    {
        Upgrade upgrade = _db_upgradeTrees.GetUpgradeByID(upgradeID);
        return CanPurchase(upgrade);
    }

    public void Purchase(string upgradeID)
    {
        Upgrade upgrade = _db_upgradeTrees.GetUpgradeByID(upgradeID);

        _profileManager.CurrentProfile.Wallet.RemoveResources(upgrade.Cost);
        _profileManager.CurrentProfile.CharacterManager.AddUpgrade(_profileManager.CurrentProfile.CharacterManager.Character_ID, upgradeID);

        onPurchase?.Invoke();
    }
}