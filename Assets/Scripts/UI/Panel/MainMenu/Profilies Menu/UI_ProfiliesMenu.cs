using UnityEngine;

public class UI_ProfiliesMenu : UI_Panel 
{
    [SerializeField] private UI_SelectProfile ui_selectProfile;
    [SerializeField] private UI_CreateProfile ui_createProfile;

    public void Initialize(GameInstance gameInstance)
    {
        ui_selectProfile.Initialize(gameInstance, this, ui_createProfile);
        ui_createProfile.Initialize(gameInstance);

        ui_selectProfile.Show();
        ui_createProfile.Hide();
    }
}