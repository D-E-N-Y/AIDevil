using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager 
{
    PlayerCharacterStats _stats;

    public UpgradesManager(PlayerCharacterStats stats)
    {
        _stats = stats;
    }

    public void ApplyUpgrade(Upgrade upgrade)
    {
        foreach (StatModifier modifier in upgrade.Modifiers)
        {
            _stats.ModifyStat(modifier.stat, modifier.value);
        }
    }

    public void ApplyUpgrades(IReadOnlyList<string> upgradesID, DB_UpgradeTrees db_upgradeTrees)
    {
        foreach (string id in upgradesID)
        {
            Upgrade upgrade = db_upgradeTrees.GetUpgradeByID(id);
            if (upgrade != null)
            {
                ApplyUpgrade(upgrade);
            }
            else
            {
                Debug.LogWarning($"Upgrade with ID {id} not found in DB_UpgradeTrees!");
            }
        }
    }
}