using System;
using System.Collections.Generic;

[Serializable]
public class DB_Profilies 
{
    public Action onUpdateDB;
    
    private List<Profile> profiles;

    public DB_Profilies()
    {
        profiles = new List<Profile>();
    }

    public DB_Profilies(List<Profile> profiles)
    {
        this.profiles = profiles;
    }   

    public void SetData(List<Profile> profiles)
    {
        this.profiles = profiles;
        UpdateDB();
    }

    public void AddProfile(Profile profile)
    {
        profiles.Add(profile);
        UpdateDB();
    }

    public void RemoveProfile(Profile profile)
    {
        profiles.Remove(profile);
        UpdateDB();
    }

    public void UpdateProfile(Profile profile)
    {
        int index = profiles.FindIndex(p => p.name == profile.name);
        if(index != -1)
        {
            profiles[index] = profile;
            UpdateDB();
        }
    }

    public bool HasProfilieByName(string name)
    {
        return profiles.Exists(profile => profile.name == name);
    }

    public Profile GetProfileByName(string name)
    {
        return profiles.Find(profile => profile.name == name);
    }

    public List<Profile> GetProfiles() => profiles;

    public bool HasProfilies() => profiles.Count > 0;

    private void UpdateDB()
    {
        // SaveLoadSystem.current.SaveGame(new SaveData(this, GameInstance.current.GetProfile()));
        onUpdateDB?.Invoke();
    }

}