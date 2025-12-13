using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UI_Profile : UI_Panel 
{
    public Action<UI_Profile> onSelect;
    
    [SerializeField] private Image ui_selectImage;
    [SerializeField] private TextMeshProUGUI ui_nameProfileText;

    private Color _selectColorText;
    private Color _defaultColorText;

    private bool isSelect;

    private Profile _profile;

    public void Initialize(Profile profile)
    {
        onSelect = null;
        
        _profile = profile;

        ui_nameProfileText.text = _profile.name;

        _selectColorText = new Color(255f / 255f, 243f / 255f, 208f / 255f);
        _defaultColorText = new Color(32f / 255f, 18f / 255f, 6f / 255f);

        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(() => Select());

        UnSelect();
    }

    public void Select()
    {
        isSelect = true;

        ui_selectImage.gameObject.SetActive(isSelect);

        ui_nameProfileText.color = _selectColorText;
        ui_nameProfileText.fontStyle = FontStyles.Bold;

        onSelect?.Invoke(this);
    }

    public void UnSelect()
    {
        isSelect = false;

        ui_selectImage.gameObject.SetActive(isSelect);
        
        ui_nameProfileText.color = _defaultColorText;
        ui_nameProfileText.fontStyle = FontStyles.Normal;
    }

    public Profile GetProfile() => _profile;
}