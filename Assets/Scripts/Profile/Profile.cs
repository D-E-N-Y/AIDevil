using System.Collections.Generic;

public class Profile
{
    private ProfileData _data;
    
    private Wallet _wallet;
    private BestiaryProgress _bestiaryProgress;
    private CharacterManager _characterManager;
    private SessionResultsProgress _sessionResultsProgress;

    public Profile()
    {
        _data = new ProfileData();
        _characterManager = new CharacterManager();
        _wallet = new Wallet();
        _sessionResultsProgress = new SessionResultsProgress();
        _bestiaryProgress = new BestiaryProgress();
    }

    public Profile(ProfileData data)
    {
        _data = data;
        _characterManager = new CharacterManager(_data.Character_ID, _data.UpgradeProgress);
        _wallet = new Wallet(_data.Resources);
        _sessionResultsProgress = new SessionResultsProgress(_data.SesionResults);
        _bestiaryProgress = new BestiaryProgress(_data.DiscoveredEnemiesNames);
    }

    public string Name => _data.Name;

    public Wallet Wallet => _wallet;
    public BestiaryProgress BestiaryProgress => _bestiaryProgress;
    public CharacterManager CharacterManager => _characterManager;
    public SessionResultsProgress SessionResultsProgress => _sessionResultsProgress;

    public ProfileData GetData()
    {
        ProfileData profileData = new ProfileData(
            _data.Name,
            _characterManager.Character_ID,
            new Dictionary<string, HashSet<string>>(_characterManager.UpgradeProgress.Progress),
            new List<SSesionResult>(_sessionResultsProgress.SesionResults),
            new List<string>(_bestiaryProgress.DiscoveredEnemiesNames),
            new Dictionary<ResourceType, int>(_wallet.Resources)
        );
        
        return profileData;
    }
}