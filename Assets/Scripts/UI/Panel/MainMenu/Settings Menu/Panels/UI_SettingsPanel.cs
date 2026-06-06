using UnityEngine;

public abstract class UI_SettingsPanel : UI_Panel 
{
    public abstract SettingsType Type { get; }
}