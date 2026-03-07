using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Stats : UI_Panel 
{
    [SerializeField] private StatIcons statIcons;
    
    [SerializeField] private Button ui_backButton;

    [SerializeField] private Transform ui_statsContainer;
    [SerializeField] private UI_Stat ui_statPrefab;

    private Dictionary<StatType, UI_Stat> ui_stats;

    private PlayerCharacterStats _stats;

    public void Initialize(PlayerCharacterStats stats, UI_PauseMenu ui_pauseMenu)
    {
        _stats = stats;

        if (ui_pauseMenu != null)
        {
            ui_backButton.onClick.RemoveAllListeners();
            ui_backButton.onClick.AddListener(() => {
                ui_pauseMenu.Show();
                Hide();
            });
        }

        DisableAllObjectsInContainer();
        CreateUIStats();
    }

    private void DisableAllObjectsInContainer()
    {
        for (int i = 0; i < ui_statsContainer.childCount; i++)
        {
            ui_statsContainer.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void CreateUIStats()
    {
        ui_stats = new Dictionary<StatType, UI_Stat>();

        foreach (StatType stat in Enum.GetValues(typeof(StatType)))
        {
            if(_stats.CurrentStats.ContainsKey(stat))
            {
                UI_Stat ui_stat = Instantiate(ui_statPrefab, ui_statsContainer);
                ui_stat.Initialize(statIcons.GetStatIcon(stat), _stats.CurrentStats[stat]);
                ui_stats.Add(stat, ui_stat);
            }
        }
    }
    
    public void SetData()
    {
        foreach (StatType stat in Enum.GetValues(typeof(StatType)))
        {
            if(ui_stats.ContainsKey(stat))
            {
                ui_stats[stat].Initialize(statIcons.GetStatIcon(stat), _stats.CurrentStats[stat]);
            }
        }
    }

    public override void Show()
    {
        base.Show();
        SetData();
    }
}