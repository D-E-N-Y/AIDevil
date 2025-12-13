using System;
using System.Collections.Generic;

using UnityEngine;

public class DB_Profilies 
{
    public Action onUpdateDB;
    
    private List<Profile> profiles;

    public DB_Profilies()
    {
        profiles = new List<Profile>();
    }

    public void AddProfile(Profile profile)
    {
        profiles.Add(profile);
        onUpdateDB?.Invoke();
    }

    public void RemoveProfile(Profile profile)
    {
        profiles.Remove(profile);
        onUpdateDB?.Invoke();
    }

    public void UpdateProfile(Profile profile)
    {
        int index = profiles.FindIndex(p => p.name == profile.name);
        if(index != -1)
        {
            profiles[index] = profile;
            onUpdateDB?.Invoke();
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
}