using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_Pause : UI_Panel 
{
    public event Action onExitSession;

    private Button ui_pauseButton;
    
    [SerializeField] private UI_PauseMenu ui_menu;
    [SerializeField] private UI_StatsMenu ui_statsMenu;
    [SerializeField] private UI_InventoryMenu ui_inventoryMenu;
    [SerializeField] private UI_Settings ui_settings;

    public void Initialize(UnitContext context, UI_Gameplay ui_gameplay)
    {
        ui_pauseButton = ui_gameplay.UIPauseButton;
        ui_pauseButton.onClick.RemoveAllListeners();
        ui_pauseButton.onClick.AddListener(() => {
            ui_gameplay.Hide();
            Show();
        });
        
        ui_menu.Initialize(this, ui_gameplay, ui_statsMenu, ui_inventoryMenu, ui_settings);
        ui_statsMenu.Initialize((PlayerCharacterStats)context.Stats, ui_menu);
        ui_inventoryMenu.Initialize(context.Inventory, context.Wallet, ui_menu);
        ui_settings.Initialize(ui_menu);

        ui_menu.Show();
        ui_statsMenu.Hide();
        ui_inventoryMenu.Hide();
        ui_settings.Hide();
    }

    public void OnExitSession()
    {
        onExitSession?.Invoke();
    }

    public override void Hide()
    {
        base.Hide();

        Time.timeScale = 1f;
    }

    public override void Show()
    {
        base.Show();

        Time.timeScale = 0f;
    }
}