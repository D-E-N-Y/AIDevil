using System;
using System.Collections.Generic;
using UnityEngine;

public class UI_Stats : UI_Panel 
{
    [SerializeField] private StatIcons statIcons;

    [SerializeField] private Transform ui_statsContainer;
    [SerializeField] private UI_Stat ui_statPrefab;

    private Dictionary<StatType, UI_Stat> ui_stats;

    private UnitStats _stats;

    private static readonly StatType[] allstats = (StatType[])Enum.GetValues(typeof(StatType));

    public void Initialize()
    {
        ui_stats = new Dictionary<StatType, UI_Stat>();

        DisableAllObjectsInContainer();
        CreateUIStats();
    }

    public void SetStats(UnitStats stats)
    {
        _stats = stats;

        if (_stats.CurrentStats == null)
        {
            _stats.Initialize();
        }
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
        foreach (StatType stat in allstats)
        {
            if (!ui_stats.ContainsKey(stat))
            {
                UI_Stat ui_stat = Instantiate(ui_statPrefab, ui_statsContainer);
                ui_stat.Initialize(statIcons.GetStatIcon(stat));
                ui_stats.Add(stat, ui_stat);
            }
        }
    }
    
    public void UpdateUI()
    {
        foreach (StatType stat in allstats)
        {
            if(ui_stats.ContainsKey(stat))
            {
                if (_stats.CurrentStats.ContainsKey(stat))
                {
                    ui_stats[stat].SetValue(_stats.CurrentStats[stat]);
                    ui_stats[stat].Show();
                }
                else
                {
                    ui_stats[stat].Hide();
                }
            }
        }
    }
}