using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public List<ProfileData> profiles;
    public ProfileData currentProfile;

    public SaveData(List<ProfileData> profiles, ProfileData currentProfile)
    {
        this.profiles = profiles;
        this.currentProfile = currentProfile;
    }
}
