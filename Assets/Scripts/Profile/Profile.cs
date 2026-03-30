using System.Collections.Generic;
using UnityEngine;

public class Profile
{
    private ProfileData _data;
    
    private Wallet _wallet;
    private BestiaryProgress _bestiaryProgress;
    private CharacterManager _characterManager;
    private SessionResultsProgress _sessionResultsProgress;
    private GameLevelsProgress _gameLevelsProgress;

    public Profile(StartResources startResources)
    {
        _data = new ProfileData();
        _characterManager = new CharacterManager();
        _wallet = new Wallet(startResources.StartResourcesList);
        _sessionResultsProgress = new SessionResultsProgress();
        _bestiaryProgress = new BestiaryProgress();
        _gameLevelsProgress = new GameLevelsProgress();
    }

    public Profile(ProfileData data)
    {
        _data = data;
        _characterManager = new CharacterManager(_data.Character_ID, _data.UnlockedCharacters, _data.UpgradeProgress);
        _wallet = new Wallet(_data.Resources);
        _sessionResultsProgress = new SessionResultsProgress(_data.SesionResults);
        _bestiaryProgress = new BestiaryProgress(_data.DiscoveredEnemiesNames);
        _gameLevelsProgress = new GameLevelsProgress(_data.GameLevelsProgress);
    }

    public string Name => _data.Name;

    public Wallet Wallet => _wallet;
    public BestiaryProgress BestiaryProgress => _bestiaryProgress;
    public CharacterManager CharacterManager => _characterManager;
    public SessionResultsProgress SessionResultsProgress => _sessionResultsProgress;
    public GameLevelsProgress GameLevelsProgress => _gameLevelsProgress;

    public ProfileData GetData()
    {
        ProfileData profileData = new ProfileData(
            _data.Name,
            _characterManager.Character_ID,
            _characterManager.CharacterProgress.UnlockedCharacters,
            new Dictionary<string, HashSet<string>>(_characterManager.UpgradeProgress.Progress),
            new List<SSesionResult>(_sessionResultsProgress.SesionResults),
            new List<string>(_bestiaryProgress.DiscoveredEnemiesNames),
            _gameLevelsProgress.CompletedLevels,
            new Dictionary<ResourceType, int>(_wallet.Resources)
        );
        
        return profileData;
    }
}