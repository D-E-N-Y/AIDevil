using UnityEngine;
using UnityEngine.UI;

public class UI_StatsMenu : UI_Panel 
{
    [SerializeField] private Button ui_backButton;
    [SerializeField] private UI_Stats ui_stats;

    public void Initialize(PlayerCharacterStats stats, UI_PauseMenu ui_pauseMenu)
    {
        ui_backButton.onClick.RemoveAllListeners();
        ui_backButton.onClick.AddListener(() => {
            ui_pauseMenu.Show();
            Hide();
        });

        ui_stats.Initialize();
        ui_stats.SetStats(stats);
    }

    public override void Show()
    {
        base.Show();
        ui_stats.UpdateUI();
    }
}