using UnityEngine;
using UnityEngine.UI;

public class UI_SettingsMenu : UI_Panel 
{
    [SerializeField] private Button ui_closeButton;

    [SerializeField] private UI_SettingsTabList ui_tabList;
    [SerializeField] private UI_SettingsPanels ui_panels;

    public void Initialize(GameInstance gameInstance)
    {
        ui_closeButton.onClick.RemoveAllListeners();
        ui_closeButton.onClick.AddListener(() => Hide()); 

        ui_panels.Initialize(gameInstance);
        ui_tabList.Initialize(ui_panels.Panels);
    }

    public void Initialize(UI_PauseMenu ui_pauseMenu, GameInstance gameInstance)
    {
        ui_closeButton.onClick.RemoveAllListeners();
        ui_closeButton.onClick.AddListener(() => {
            ui_pauseMenu.Show();
            Hide();
        });

        ui_panels.Initialize(gameInstance);
        ui_tabList.Initialize(ui_panels.Panels);
    }
}