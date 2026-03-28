using System.Collections.Generic;

[System.Serializable]
public class BestiaryProgress
{
    private List<string> _discoveredEnemiesNames;
    public IReadOnlyList<string> DiscoveredEnemiesNames => _discoveredEnemiesNames;

    public BestiaryProgress()
    {
        _discoveredEnemiesNames = new List<string>();
    }

    public BestiaryProgress(List<string> discoveredEnemiesNames)
    {
        if (discoveredEnemiesNames == null)
        {
            _discoveredEnemiesNames = new List<string>();
        }
        else
        {
            _discoveredEnemiesNames = discoveredEnemiesNames;
        }
    }

    public void AddEnemy(string enemyName)
    {
        if (!_discoveredEnemiesNames.Contains(enemyName))
        {
            _discoveredEnemiesNames.Add(enemyName);
        }
    }

    public bool HasAnyDiscoveredEnemies()
    {
        return _discoveredEnemiesNames.Count > 0;
    }
}