using System.Collections.Generic;
using UnityEngine;

public class UpgradeContainer
{
    private List<string> _upgradesID;
    public IReadOnlyList<string> Upgrades_ID => _upgradesID;

    public UpgradeContainer()
    {
        _upgradesID = new List<string>();
    }

    public UpgradeContainer(List<string> upgradesID)
    {
        if (upgradesID == null)
        {
            _upgradesID = new List<string>();
        }
        else
        {
            _upgradesID = upgradesID;
        }
    }

    public void AddUpgrade(Upgrade upgrade)
    {
        if (upgrade == null)
        {
            Debug.LogWarning("Upgrade at time add to list was NULL!!!");
            return;
        }

        if (!_upgradesID.Contains(upgrade.ID))
        {
            _upgradesID.Add(upgrade.ID);
        }
    }

    public void AddUpgrade(string upgrade)
    {
        if (upgrade == null)
        {
            Debug.LogWarning("Upgrade at time add to list was NULL!!!");
            return;
        }

        if (!_upgradesID.Contains(upgrade))
        {
            _upgradesID.Add(upgrade);
        }
    }

    public void RemoveUpgrade(Upgrade upgrade)
    {
        if (upgrade == null)
        {
            Debug.LogWarning("Upgrade at time remove from list was NULL!!!");
            return;
        }

        if (_upgradesID.Contains(upgrade.ID))
        {
            _upgradesID.Remove(upgrade.ID);
        }
    }

    public bool HasUpgrade(Upgrade upgrade)
    {
        return _upgradesID.Contains(upgrade.ID);
    }
}