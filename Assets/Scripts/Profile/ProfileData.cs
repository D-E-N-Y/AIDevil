using System;
using System.Collections.Generic;

[Serializable]
public class ProfileData
{
    public string Name;
    public string Character_ID;
    public Dictionary<string, HashSet<string>> UpgradeProgress;
    public List<SSesionResult> SesionResults;
    public List<string> DiscoveredEnemiesNames;
    public Dictionary<ResourceType, int> Resources;

    public ProfileData()
    {
        Name = "";
        Character_ID = "";
        UpgradeProgress = new Dictionary<string, HashSet<string>>();
        SesionResults = new List<SSesionResult>();
        DiscoveredEnemiesNames = new List<string>();
        Resources = new Dictionary<ResourceType, int>();
    }

    public ProfileData(string name)
    {
        Name = name;
        Character_ID = "";
        UpgradeProgress = new Dictionary<string, HashSet<string>>();
        SesionResults = new List<SSesionResult>();
        DiscoveredEnemiesNames = new List<string>();
        Resources = new Dictionary<ResourceType, int>();
    }

    public ProfileData(
        string name, 
        string characterID, 
        Dictionary<string, HashSet<string>> upgradeProgress, 
        List<SSesionResult> sesionResults, 
        List<string> bestiaryProgress, 
        Dictionary<ResourceType, int> resources)
    {
        Name = name;
        Character_ID = characterID;
        UpgradeProgress = upgradeProgress;
        SesionResults = sesionResults;
        DiscoveredEnemiesNames = bestiaryProgress;
        Resources = resources;
    }
}