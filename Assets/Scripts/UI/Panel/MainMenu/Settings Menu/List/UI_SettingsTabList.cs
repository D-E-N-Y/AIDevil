using System.Collections.Generic;
using UnityEngine;

public class UI_SettingsTabList : UI_Panel 
{
    [SerializeField] private UI_SettingsTabButton ui_tabButtonPrefab;
    [SerializeField] private Transform ui_tabButtonContainer;

    private List<UI_SettingsTabButton> ui_tabButtons;
    private UI_SettingsTabButton _currentTabButton;
    
    public void Initialize(IReadOnlyDictionary<SettingsType, UI_SettingsPanel> panels)
    {
        CreateTabButtons(panels);
        ShowFirstTab();
    }

    private void ShowFirstTab()
    {
        if (ui_tabButtons.Count > 0)
        {
            ui_tabButtons[0].Select();
        }
    }

    private void CreateTabButtons(IReadOnlyDictionary<SettingsType, UI_SettingsPanel> panels)
    {
        ui_tabButtons = new List<UI_SettingsTabButton>();

        DisableChildsInContainer();

        foreach (var panel in panels)
        {
            UI_SettingsTabButton tabButtob = Instantiate(ui_tabButtonPrefab, ui_tabButtonContainer);
            tabButtob.Initialize(panel.Key, panel.Value);
            tabButtob.onSelect += OnTabSelected;

            ui_tabButtons.Add(tabButtob);
        }
    }

    private void DisableChildsInContainer()
    {
        for (int i = 0; i < ui_tabButtonContainer.childCount; i++)
        {
            ui_tabButtonContainer.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void OnTabSelected(UI_SettingsTabButton selectTabButton)
    {
        if (selectTabButton == _currentTabButton) return;

        _currentTabButton?.UnSelect();
        _currentTabButton?.Panel?.Hide();

        _currentTabButton = selectTabButton;
        _currentTabButton.Panel?.Show();
    }
}