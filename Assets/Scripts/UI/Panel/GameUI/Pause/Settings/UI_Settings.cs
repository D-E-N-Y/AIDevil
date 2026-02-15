using UnityEngine;
using UnityEngine.UI;

public class UI_Settings : UI_Panel 
{
    [SerializeField] private Button ui_backButton;

    public void Initialize(UI_PauseMenu ui_pauseMenu)
    {
        ui_backButton.onClick.RemoveAllListeners();
        ui_backButton.onClick.AddListener(() => {
            ui_pauseMenu.Show();
            Hide();
        });
    }
}