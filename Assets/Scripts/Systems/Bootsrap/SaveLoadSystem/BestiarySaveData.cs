using System.Collections.Generic;

[System.Serializable]
public struct BestiarySaveData
{
    public List<string> discoveredEnemiesNames;

    public BestiarySaveData(List<string> discoveredEnemiesNames)
    {
        this.discoveredEnemiesNames = discoveredEnemiesNames;
    }

    public void AddDiscoveredEnemy(string enemyName)
    {
        if (!discoveredEnemiesNames.Contains(enemyName))
        {
            discoveredEnemiesNames.Add(enemyName);
        }
    }

    public bool HasAnyDiscoveredEnemies()
    {
        return discoveredEnemiesNames.Count > 0;
    }
}