using System.Collections.Generic;
using UnityEngine;

public class UpgradeContainer
{
    private List<Upgrade> _upgrades;
    public IReadOnlyList<Upgrade> Upgrades => _upgrades;

    public UpgradeContainer()
    {
        _upgrades = new List<Upgrade>();
    }

    public void AddUpgrade(Upgrade upgrade)
    {
        if (upgrade == null)
        {
            Debug.LogWarning("Upgrade at time add to list was NULL!!!");
            return;
        }

        if (!_upgrades.Contains(upgrade))
        {
            _upgrades.Add(upgrade);
        }
    }

    public void RemoveUpgrade(Upgrade upgrade)
    {
        if (upgrade == null)
        {
            Debug.LogWarning("Upgrade at time remove from list was NULL!!!");
            return;
        }

        if (_upgrades.Contains(upgrade))
        {
            _upgrades.Remove(upgrade);
        }
    }

    public bool HasUpgrade(Upgrade upgrade)
    {
        return _upgrades.Contains(upgrade);
    }
}