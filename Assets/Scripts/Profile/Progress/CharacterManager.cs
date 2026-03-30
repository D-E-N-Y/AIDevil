using System.Collections.Generic;
using System.Linq;

public class CharacterManager
{
    private string _characterID;

    private CharacterProgress _characterProgress;

    private UpgradeContainer _upgradeContainer;
    private UpgradeProgress _upgradeProgress;

    public CharacterManager()
    {
        _characterID = null;
        _characterProgress = new CharacterProgress();
        _upgradeContainer = new UpgradeContainer();
        _upgradeProgress = new UpgradeProgress();
    }

    public CharacterManager(string characterID, HashSet<string> unlockedCharacters, Dictionary<string, HashSet<string>> progress)
    {
        _characterID = characterID;

        _characterProgress = new CharacterProgress(unlockedCharacters);
        _upgradeProgress = new UpgradeProgress(progress);

        _upgradeContainer = new UpgradeContainer(
            _upgradeProgress.GetUpgradesByCharacterID(_characterID).ToList()
        );
    }

    public void SetCharacter(string characterID)
    {
        _characterID = characterID;

        _upgradeContainer = new UpgradeContainer(
            _upgradeProgress.GetUpgradesByCharacterID(_characterID).ToList()
        );
    }

    public void AddUpgrade(string characterID, string upgradeID)
    {
        _upgradeProgress.AddUpgrade(characterID, upgradeID);

        if (_characterID == characterID)
        {
            _upgradeContainer.AddUpgrade(upgradeID);
        }
    }

    public int GetCharacterLevel()
    {
        return _upgradeContainer.GetUpgradesCount() + 1;
    }

    public string Character_ID => _characterID;
    public CharacterProgress CharacterProgress => _characterProgress;
    public UpgradeContainer UpgradeContainer => _upgradeContainer;
    public UpgradeProgress UpgradeProgress => _upgradeProgress;
}