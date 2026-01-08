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
        if (_profiles.Exists(p => p.name == profile.name)) return;

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
        if(_currentProfile.name == profile.name) return;

        UpdateCurrentProfileInList();

        _currentProfile = profile;

        onCurrentProfileChanged?.Invoke();
    }

    private void UpdateCurrentProfile()
    {
        if(!HasProfilieByName(_currentProfile.name))
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
        int index = _profiles.FindIndex(p => p.name == _currentProfile.name);
        if(index >= 0)
        {
            _profiles[index] = _currentProfile;
        }
    }

    public bool HasProfilieByName(string name)
    {
        return _profiles.Exists(p => p.name == name);
    }

    public bool HasProfilies() => _profiles.Count > 0;

    public bool IsValidProfile() => HasProfilieByName(_currentProfile.name) && _currentProfile.playerCharacterName != string.Empty;

    public Profile GetProfile() => _currentProfile;
    public IReadOnlyList<Profile> GetProfiles() => _profiles;

    #endregion
    

    #region Session Results Management

    public void AddSessionResult(SSesionResult sessionResult)
    {
        _currentProfile.sessionResults.Add(sessionResult);
    }

    public bool HasSessionResultsCurrentProfile() => _currentProfile.sessionResults.Count > 0;

    public IReadOnlyList<SSesionResult> GetSessionResultsCurrentProfile() => _currentProfile.sessionResults;

    #endregion

    
    #region Player Character Management

    public void SetPlayer(Player player)
    {
        if (player == null)
        {
            Debug.Log("!!! select player is null");
            return;
        }

        _currentProfile.playerCharacterName = player.GetName();
    }

    public Player GetPlayerCharacter()
    {
        return _dataBase.Characters.GetCharacterByName(_currentProfile.playerCharacterName);
    }

    #endregion

    
    #region Data Management

    private void CheckData()
    {
        SaveData saveData = _saveLoadSystem.LoadGame();
        
        if (saveData == null)
        {
            CreateNewData();
        }
        else
        {
            LoadData(saveData);
        }
    }

    private void LoadData(SaveData saveData)
    {
        _currentProfile = saveData.currentProfile;
        _profiles = saveData.profiles;
    }

    private void CreateNewData()
    {
        _currentProfile = new Profile();
        _profiles = new List<Profile>();
    }

    public void SaveData()
    {
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


    public void ClearSubscriptions()
    {
        onUpdateProfiles = null;
    }
}