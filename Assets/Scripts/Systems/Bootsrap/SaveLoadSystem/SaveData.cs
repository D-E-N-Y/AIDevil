using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public List<Profile> profiles;
    public Profile currentProfile;

    public SaveData(List<Profile> profiles, Profile currentProfile)
    {
        this.profiles = profiles;
        this.currentProfile = currentProfile;
    }
}
