using System.Collections.Generic;
using UnityEngine;

public class UI_SettingsPanels : UI_Panel 
{
    [SerializeField] private UI_AudioPanel audioPanel;
    private Dictionary<SettingsType, UI_SettingsPanel> _panelsDictionary;

    public void Initialize(GameInstance gameInstance)
    {
        audioPanel.Initialize(gameInstance.AudioSystem);
        audioPanel.Hide();
        
        _panelsDictionary = new Dictionary<SettingsType, UI_SettingsPanel>();
        _panelsDictionary.Add(audioPanel.Type, audioPanel);
    }

    public IReadOnlyDictionary<SettingsType, UI_SettingsPanel> Panels => _panelsDictionary;
}