using System;
using UnityEngine;

public class GameInstance : MonoBehaviour
{
    public static GameInstance current;

    public Action onUpdateProfile;

    [SerializeField] private DB_Characters db_characters;
    private DB_Profilies db_profilies;
    private Profile currentProfile;

    public void Initialize()
    {
        current = this;
        DontDestroyOnLoad(this);
        
        db_profilies = new DB_Profilies();
        currentProfile = new Profile(null, null, GetCopyDBCharacters(), new DB_SessionResults());

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

    public bool IsValidProfile()
    {
        return !string.IsNullOrEmpty(currentProfile.name);
    }

    public Profile GetProfile()
    {
        return currentProfile;
    }

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
                SetProfile(new Profile(null, null, GetCopyDBCharacters(), new DB_SessionResults()));
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

        currentProfile.playerCharacter = player;
    }
    
    public DB_Profilies DBProfilies() => db_profilies;

    public Player GetPlayerCharacter() => currentProfile.playerCharacter;
    
    public DB_Characters DBCharacters() => currentProfile.db_characters;
    public DB_Characters GetCopyDBCharacters() => Instantiate(db_characters);

    public DB_SessionResults DBSessionResults() => currentProfile.db_sessionResults;

    public void ClearSubscriptions()
    {
        onUpdateProfile = null;
    }
}