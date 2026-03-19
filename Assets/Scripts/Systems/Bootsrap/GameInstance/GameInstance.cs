using System;
using System.Collections.Generic;
using UnityEngine;

public class GameInstance : MonoBehaviour
{
    public static GameInstance current;

    public Action onUpdateProfiles;
    public Action onCurrentProfileChanged;
    
    private Profile _currentProfile;
    private List<Profile> _profiles;

    private Wallet _profileWallet;
    public Wallet ProfileWallet => _profileWallet;

    private GameLevel _currentGameLevel;
    public GameLevel CurrentGameLevel => _currentGameLevel;

    private SaveLoadSystem _saveLoadSystem;
    private DataBase _dataBase;

    public void Initialize(DataBase dataBase)
    {
        current = this;
        DontDestroyOnLoad(this);

        _saveLoadSystem = new SaveLoadSystem();
        _dataBase = dataBase;

        CheckData();
    }
    
    #region Profiles Management
    
    public void AddProfile(Profile profile)
    {
        if (_profiles.Exists(p => p.Name == profile.Name)) return;

        _profiles.Add(profile);
        onUpdateProfiles?.Invoke();
    }

    public void RemoveProfile(Profile profile)
    {
        _profiles.Remove(profile);
        
        UpdateCurrentProfile();

        onUpdateProfiles?.Invoke();
    }

    public void SetProfile(Profile profile)
    {
        if(_currentProfile.Name == profile.Name) return;

        UpdateCurrentProfileInList();

        _currentProfile.SetResources(new Dictionary<ResourceType, int>(_profileWallet.Resources));
        _currentProfile = profile;

        _profileWallet = new Wallet(new Dictionary<ResourceType, int>(_currentProfile.Resources));

        onCurrentProfileChanged?.Invoke();
    }

    private void UpdateCurrentProfile()
    {
        if(!HasProfilieByName(_currentProfile.Name))
        {
            if(HasProfilies())
            {
                SetProfile(_profiles[0]);
            }
            else
            {
                SetProfile(new Profile());
            }
        }
    }

    private void UpdateCurrentProfileInList()
    {
        int index = _profiles.FindIndex(p => p.Name == _currentProfile.Name);
        if(index >= 0)
        {
            _profiles[index] = _currentProfile;
        }
    }

    public bool HasProfilieByName(string name)
    {
        return _profiles.Exists(p => p.Name == name);
    }

    public bool HasProfilies() => _profiles.Count > 0;

    public bool IsValidProfile() => HasProfilieByName(_currentProfile.Name) && _currentProfile.PlayerCharacterName != string.Empty;

    public Profile GetProfile() => _currentProfile;
    public IReadOnlyList<Profile> GetProfiles() => _profiles;

    #endregion
    

    #region Session Results Management

    public void AddSessionResult(SSesionResult sessionResult)
    {
        _currentProfile.AddSessionResult(sessionResult);
    }

    public bool HasSessionResultsCurrentProfile() => _currentProfile.SesionResults.Count > 0;

    public IReadOnlyList<SSesionResult> GetSesionResults() => _currentProfile.SesionResults;

    #endregion

    
    #region Player Character Management

    public void SetPlayer(PlayerCharacter player)
    {
        if (player == null)
        {
            Debug.Log("!!! select player is null");
            return;
        }

        _currentProfile.SetPlayerCharacterName(player.GetName());
    }

    public PlayerCharacter GetPlayerCharacter()
    {
        return _dataBase.Characters.GetCharacterByName(_currentProfile.PlayerCharacterName);
    }

    #endregion

    
    #region Data Management

    private void CheckData()
    {
        SaveData saveData = _saveLoadSystem.LoadGame();
        
        if (saveData == null)
        {
            Debug.Log("Create new Data");
            
            CreateNewData();
        }
        else
        {
            Debug.Log("Load Data");

            LoadData(saveData);
        }

        if (_currentProfile == null)
        {
            Debug.LogWarning("Current Profile is null!!!");
        }
    }

    private void LoadData(SaveData saveData)
    {
        Debug.Log($"{saveData.currentProfile} {saveData.currentProfile.Name} {saveData.currentProfile.SesionResults} {saveData.currentProfile.BestiaryData} {saveData.currentProfile.PlayerCharacterName} {saveData.currentProfile.Resources}");

        if (saveData.currentProfile.SesionResults == null || saveData.currentProfile.Resources == null)
        {
            Debug.LogWarning("Load data is bad. Create new data!");

            CreateNewData();
            return;
        }
        
        _currentProfile = saveData.currentProfile;
        _profiles = saveData.profiles;

        _profileWallet = new Wallet(new Dictionary<ResourceType, int>(_currentProfile.Resources));
    }

    private void CreateNewData()
    {
        _currentProfile = new Profile();
        _profiles = new List<Profile>();

        _profileWallet = new Wallet(new Dictionary<ResourceType, int>(_currentProfile.Resources));
    }

    public void SaveData()
    {
        _currentProfile.SetResources(new Dictionary<ResourceType, int>(_profileWallet.Resources));
        
        _saveLoadSystem.SaveGame(
            new SaveData(
                _profiles, 
                _currentProfile
            )
        );
    }

    #endregion
    

    #region DataBase Management

    public DataBase GetDataBase() => _dataBase;

    #endregion


    #region Game Levels

    public void SetCurrentGameLevel(string nameGameLevel)
    {
        _currentGameLevel = _dataBase.GameLevels.GetGameLevelByName(nameGameLevel);
    }

        public void SetCurrentGameLevel(GameLevel gameLevel)
    {
        _currentGameLevel = gameLevel;
    }

    #endregion


    #region Wallet

    

    #endregion


    public void ClearSubscriptions()
    {
        onUpdateProfiles = null;
        onCurrentProfileChanged = null;
    }
}