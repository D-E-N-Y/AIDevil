using UnityEngine;

public class GameInstance : MonoBehaviour
{
    public static GameInstance current;

    private ProfileManager _profileManager;
    private GameLevelsManager _gameLevelsManager;
    private SaveLoadSystem _saveLoadSystem;
    private DataBase _dataBase;

    public void Initialize(DataBase dataBase)
    {
        current = this;
        DontDestroyOnLoad(this);

        _dataBase = dataBase;

        _saveLoadSystem = new SaveLoadSystem(this);

        _gameLevelsManager = new GameLevelsManager(_dataBase.GameLevels);

        InitializeData();
    }

    private void InitializeData()
    {
        SaveData data = _saveLoadSystem.LoadData();

        if (data == null)
        {
            _profileManager = new ProfileManager();
        }
        else
        {
            _profileManager = new ProfileManager(data.profiles, data.currentProfile);
        }
    }

    public SaveLoadSystem SaveLoadSystem => _saveLoadSystem;
    public ProfileManager ProfileManager => _profileManager;
    public GameLevelsManager GameLevelsManager => _gameLevelsManager;
    public DataBase DataBase => _dataBase;
}