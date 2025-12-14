using System;
using System.Collections.Generic;
using UnityEngine;

public class GameInstance : MonoBehaviour
{
    public static GameInstance current;

    public Action onUpdateProfile;

    private Profile currentProfile;

    private SaveLoadSystem _saveLoadSystem;
    private DataBase _dataBase;

    public void Initialize(SaveLoadSystem saveLoadSystem, DataBase dataBase)
    {
        current = this;
        DontDestroyOnLoad(this);
        
        _saveLoadSystem = saveLoadSystem;
        _dataBase = dataBase;

        SaveData saveData = _saveLoadSystem.LoadGame();
        if (saveData == null)
        {
            currentProfile = new Profile(
                null,
                null,
                new List<SSesionResult>()
            );
        }
        else
        {
            _dataBase.Profilies.SetData(saveData.profiles);
            _dataBase.SessionResults.SetData(saveData.currentProfile.sessionResults);
            currentProfile = saveData.currentProfile;
        }

        _dataBase.Profilies.onUpdateDB += UpdateCurrentProfile; 
    }

    public void SetProfile(Profile profile)
    {
        if(currentProfile.name == profile.name) return;

        Profile _profile = currentProfile;
        currentProfile = profile;

        _dataBase.Profilies.UpdateProfile(_profile);
        _dataBase.SessionResults.SetData(profile.sessionResults);
        
        onUpdateProfile?.Invoke();
    }

    public bool IsValidProfile() => !string.IsNullOrEmpty(currentProfile.name);
    public Profile GetProfile() => currentProfile;

    private void UpdateCurrentProfile()
    {
        if(!_dataBase.Profilies.HasProfilieByName(currentProfile.name))
        {
            if(_dataBase.Profilies.HasProfilies())
            {
                SetProfile(_dataBase.Profilies.GetProfiles()[0]);
            }
            else
            {
                SetProfile(
                    new Profile(
                        null, 
                        null, 
                        new List<SSesionResult>()
                    )
                );
            }
        }
    }

    public void SetPlayer(Player player)
    {
        if (player == null)
        {
            Debug.Log("!!! select player is null");
            return;
        }

        currentProfile.playerCharacterName = player.GetName();

        // _saveLoadSystem.SaveGame(new SaveData(db_profilies, currentProfile));
    }

    public Player GetPlayerCharacter()
    {
        return _dataBase.Characters.GetCharacterByName(currentProfile.playerCharacterName);
    }

    public void ClearSubscriptions()
    {
        onUpdateProfile = null;
    }
}