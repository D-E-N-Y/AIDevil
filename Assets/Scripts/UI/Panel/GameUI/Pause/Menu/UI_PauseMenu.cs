using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_PauseMenu : UI_Panel 
{
    [SerializeField] private Button ui_statsButton;
    [SerializeField] private Button ui_inventoryButton;
    [SerializeField] private Button ui_settingsButton;
    [SerializeField] private Button ui_exitButton;
    [SerializeField] private Button ui_continueButton;
    [SerializeField] private Button ui_restartButton;


    public void Initialize(UI_Pause ui_pause, UI_Gameplay ui_gameplay, UI_StatsMenu ui_statsMenu, UI_InventoryMenu ui_inventoryMenu, UI_Settings ui_settings)
    {
        ui_statsButton.onClick.RemoveAllListeners();
        ui_statsButton.onClick.AddListener(() =>
        {
            ui_statsMenu.Show();
            Hide();
        });

        ui_inventoryButton.onClick.RemoveAllListeners();
        ui_inventoryButton.onClick.AddListener(() =>
        {
            ui_inventoryMenu.Show();
            Hide();
        });

        ui_settingsButton.onClick.RemoveAllListeners();
        ui_settingsButton.onClick.AddListener(() =>
        {
            ui_settings.Show();
            Hide();
        });

        ui_continueButton.onClick.RemoveAllListeners();
        ui_continueButton.onClick.AddListener(() => {
            ui_gameplay.Show();
            ui_pause.Hide();
        });

        ui_restartButton.onClick.RemoveAllListeners();
        ui_restartButton.onClick.AddListener(() => SceneManager.LoadScene(GameInstance.current.GameLevelsManager.CurrentGameLevel.Name));
        
        ui_exitButton.onClick.RemoveAllListeners();
        ui_exitButton.onClick.AddListener(() => 
            {
                ui_pause.Hide();
                ui_pause.OnExitSession();
            }
        );
    }
}