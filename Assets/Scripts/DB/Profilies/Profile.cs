using System;
using System.Collections.Generic;

[Serializable]
public class Profile
{
    private string _name;
    public string Name => _name;

    private string _playerCharacterName;
    public string PlayerCharacterName => _playerCharacterName;

    private List<SSesionResult> _sessionResults;
    public IReadOnlyList<SSesionResult> SesionResults => _sessionResults;

    private BestiarySaveData _bestiaryData;
    public BestiarySaveData BestiaryData => _bestiaryData;

    private Dictionary<ResourceType, int> _resources;
    public IReadOnlyDictionary<ResourceType, int> Resources => _resources;

    public Profile()
    {
        _name = null;
        _playerCharacterName = null;
        _sessionResults = new List<SSesionResult>();
        _bestiaryData = new BestiarySaveData(new List<string>());
        
        _resources = new Dictionary<ResourceType, int>();
        foreach (ResourceType resource in Enum.GetValues(typeof(ResourceType)))
        {
            _resources[resource] = 0;
        }
    }

    public Profile(string name)
    {
        _name = name;
        _playerCharacterName = null;
        _sessionResults = new List<SSesionResult>();
        _bestiaryData = new BestiarySaveData(new List<string>());

        _resources = new Dictionary<ResourceType, int>();
        foreach (ResourceType resource in Enum.GetValues(typeof(ResourceType)))
        {
            _resources[resource] = 0;
        }
    }

    public void AddSessionResult(SSesionResult sessionResult)
    {
        _sessionResults.Add(sessionResult);
    }

    public void SetPlayerCharacterName(string pc_name)
    {
        _playerCharacterName = pc_name;
    }

    public void SetResources(Dictionary<ResourceType, int> resources)
    {
        _resources = resources;
    }

    // public Profile(string name, string playerCharacterName, List<SSesionResult> sessionResults, BestiarySaveData? bestiaryData = null)
    // {
    //     this.name = name;
    //     this.playerCharacterName = playerCharacterName;
    //     this.sessionResults = sessionResults;
    //     this.bestiaryData = bestiaryData ?? new BestiarySaveData(new List<string>());
    // }
}