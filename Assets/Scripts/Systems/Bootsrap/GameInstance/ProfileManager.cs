using System;
using System.Collections.Generic;
using UnityEngine;

public class ProfileManager
{
    public event Action onUpdateProfiles;
    public event Action onCurrentProfileChanged;
    
    private Profile _currentProfile;
    public Profile CurrentProfile => _currentProfile;

    private List<Profile> _profiles;
    public IReadOnlyList<Profile> Profiles => _profiles;

    private StartResources _startResources;

    public ProfileManager(StartResources startResources)
    {
        _startResources = startResources;
        
        _profiles = new List<Profile>();
        _currentProfile = new Profile(_startResources);
    }

    public ProfileManager(StartResources startResources, List<Profile> profiles, Profile currentProfile)
    {
        _startResources = startResources;

        _profiles = profiles;
        _currentProfile = currentProfile;
    }

    public ProfileManager(StartResources startResources, List<ProfileData> profilesData, ProfileData currentProfileData)
    {
        _startResources = startResources;

        _profiles = new List<Profile>();
        
        Profile currentProfile = null;
        foreach (ProfileData data in profilesData)
        {
            Profile profile = new Profile(data);

            if (profile.Name == currentProfileData.Name)
            {
                currentProfile = profile;
            }

            _profiles.Add(profile);
        }

        SetProfile(currentProfile);
    }

    public void AddProfile(Profile profile)
    {
        if (_profiles.Exists(p => p.Name == profile.Name)) return;

        _profiles.Add(profile);

        if (_profiles.Count == 1)
        {
            SetProfile(profile);
        }

        onUpdateProfiles?.Invoke();
    }

    public void AddProfile(string name)
    {
        if (_profiles.Exists(p => p.Name == name)) return;

        Profile profile = new Profile(new ProfileData(name, _startResources));

        _profiles.Add(profile);

        if (_profiles.Count == 1)
        {
            SetProfile(profile);
        }

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
        if (_currentProfile != null)
            if(_currentProfile.Name == profile.Name) return;

        UpdateCurrentProfileInList();

        _currentProfile = profile;

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
                SetProfile(new Profile(_startResources));
            }
        }
    }

    private void UpdateCurrentProfileInList()
    {
        if (_currentProfile == null) return;
        
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

    public bool IsValidProfile() => HasProfilieByName(_currentProfile.Name);

    public void ClearSubscriptions()
    {
        onUpdateProfiles = null;
        onCurrentProfileChanged = null;
    }

    public SaveData GetData()
    {
        List<ProfileData> profilesData = new List<ProfileData>();

        foreach (Profile profile in _profiles)
        {
            profilesData.Add(profile.GetData());
        }

        SaveData data = new SaveData(
            profilesData,
            _currentProfile.GetData()
        );

        return data;
    }
}