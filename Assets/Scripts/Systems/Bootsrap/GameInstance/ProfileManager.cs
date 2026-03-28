using System;
using System.Collections.Generic;

public class ProfileManager
{
    public event Action onUpdateProfiles;
    public event Action onCurrentProfileChanged;
    
    private Profile _currentProfile;
    public Profile CurrentProfile => _currentProfile;

    private List<Profile> _profiles;
    public IReadOnlyList<Profile> Profiles => _profiles;

    public ProfileManager()
    {
        _profiles = new List<Profile>();
        _currentProfile = new Profile();
    }

    public ProfileManager(List<Profile> profiles, Profile currentProfile)
    {
        _profiles = profiles;
        _currentProfile = currentProfile;
    }

    public ProfileManager(List<ProfileData> profilesData, ProfileData currentProfileData)
    {
        _profiles = new List<Profile>();
        foreach (ProfileData data in profilesData)
        {
            Profile profile = new Profile(data);
            _profiles.Add(profile);
        }

        _currentProfile = new Profile(currentProfileData);
    }

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

    public bool IsValidProfile() => HasProfilieByName(_currentProfile.Name) && _currentProfile.CharacterManager.Character_ID != string.Empty;

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