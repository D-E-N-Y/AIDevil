using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class UpgradeProgress
{
    private Dictionary<string, HashSet<string>> _progress;
    public IReadOnlyDictionary<string, HashSet<string>> Progress => _progress;

    public UpgradeProgress()
    {
        _progress = new Dictionary<string, HashSet<string>>();
    }

    public UpgradeProgress(Dictionary<string, HashSet<string>> progress)
    {
        if (progress == null)
        {
            _progress = new Dictionary<string, HashSet<string>>();
        }
        else
        {
            _progress = progress;
        }
    }

    public void AddUpgrade(string characterID, string upgradeID)
    {
        if (!_progress.ContainsKey(characterID))
        {
            _progress[characterID] = new HashSet<string>();
        }
        
        _progress[characterID].Add(upgradeID);
    }

    public bool HasUpgrade(string characterID, string upgradeID)
    {
        return _progress.TryGetValue(characterID, out var upgrades) &&
            upgrades.Contains(upgradeID);
    }

    public bool HasAnyUpgrades(string characterID)
    {
        return _progress.TryGetValue(characterID, out var upgrades) &&
            upgrades.Count > 0;
    }

    public IReadOnlyList<string> GetUpgradesByCharacterID(string characterID)
    {
        _progress.TryGetValue(characterID, out var upgrades);
        {
            if (upgrades == null)
            {
                return new List<string>();
            }

            return upgrades.ToList();
        }
    }
}