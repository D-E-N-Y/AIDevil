using System;
using Unity.Barracuda;
using UnityEngine;

public class GameInstance : MonoBehaviour
{
    public static GameInstance current;

    public Action onUpdateProfile;

    [SerializeField] private DB_Characters db_characters;

    private DB_Profilies db_profilies;
    private Profile currentProfile;

    private SaveLoadSystem _saveLoadSystem;

    public void Initialize(SaveLoadSystem saveLoadSystem)
    {
        current = this;
        DontDestroyOnLoad(this);
        
        _saveLoadSystem = saveLoadSystem;

        SaveData saveData = _saveLoadSystem.LoadGame();
        if (saveData == null)
        {
            Debug.Log("[GameInstance] New Game");

            db_profilies = new DB_Profilies();
            currentProfile = new Profile(
                null,
                null,
                new DB_SessionResults()
            );
        }
        else
        {
            Debug.Log("[GameInstance] Load Game");

            db_profilies = new DB_Profilies(saveData.profiles);
            currentProfile = saveData.currentProfile;
        }

        Debug.Log($"[GameInstance]\n DB Session Results : {currentProfile.db_sessionResults.HasRecords()}");

        db_profilies.onUpdateDB += UpdateCurrentProfile; 
    }

    public void SetProfile(Profile profile)
    {
        if(currentProfile.name == profile.name) return;

        Profile _profile = currentProfile;
        currentProfile = profile;
        
        db_profilies.UpdateProfile(_profile);
        
        onUpdateProfile?.Invoke();
    }

    public bool IsValidProfile() => !string.IsNullOrEmpty(currentProfile.name);
    public Profile GetProfile() => currentProfile;

    private void UpdateCurrentProfile()
    {
        if(!db_profilies.HasProfilieByName(currentProfile.name))
        {
            if(db_profilies.HasProfilies())
            {
                SetProfile(db_profilies.GetProfiles()[0]);
            }
            else
            {
                SetProfile(
                    new Profile(
                        null, 
                        null, 
                        new DB_SessionResults()
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
    
    public DB_Profilies DBProfilies() => db_profilies;
    public Player GetPlayerCharacter() => db_characters.GetCharacterByName(currentProfile.playerCharacterName);
    public DB_Characters DBCharacters() => db_characters;
    public DB_SessionResults DBSessionResults() => currentProfile.db_sessionResults;
    public DB_Characters GetCopyDBCharacters() => Instantiate(db_characters);
    // public bool IsNewGame() => isNewGame; 

    public void ClearSubscriptions()
    {
        onUpdateProfile = null;
    }
}