
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

[RequireComponent(typeof(Button))]
public class UI_SettingsTabButton : UI_Panel 
{
    private SettingsType settingsType;
    
    public Action<UI_SettingsTabButton> onSelect;
    
    [SerializeField] private Image ui_selectImage;
    [SerializeField] private Image ui_unselectImage;
    private bool _isSelect;

    [SerializeField] private TextMeshProUGUI ui_nameSessionText;

    private UI_SettingsPanel _panel;
    
    public void Initialize(SettingsType type,UI_SettingsPanel panel)
    {
        _panel = panel;
        
        onSelect = null;
        ui_nameSessionText.text = settingsType.ToString();

        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(() => Select());

        UnSelect();
    }

    public void Select()
    {
        _isSelect = true;
        
        ui_selectImage.gameObject.SetActive(_isSelect);
        ui_unselectImage.gameObject.SetActive(!_isSelect);
        
        ui_nameSessionText.fontStyle = FontStyles.Normal;

        onSelect?.Invoke(this);
    }

    public void UnSelect()
    {
        _isSelect = false;
        
        ui_selectImage.gameObject.SetActive(_isSelect);
        ui_unselectImage.gameObject.SetActive(!_isSelect);
        
        ui_nameSessionText.fontStyle = FontStyles.Normal;
    }

    public SettingsType Type => settingsType;
    public UI_SettingsPanel Panel => _panel;
}