using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_GameLevel : UI_Panel 
{
    public event Action<UI_GameLevel> onSelect;  
    
    [SerializeField] private TextMeshProUGUI ui_nameText;

    [SerializeField] private Image ui_selectImage;
    [SerializeField] private Image ui_unSelectImage;

    [SerializeField] private Button ui_selectButton;

    private bool _isSelect;
    public bool IsSelect => _isSelect;

    private string _nameGameLevel;
    public string NameGameLevel => _nameGameLevel;

    public void Initialize(string nameGameLevel)
    {
        _nameGameLevel = nameGameLevel;

        ui_nameText.text = _nameGameLevel;

        ui_selectButton.onClick.RemoveAllListeners();
        ui_selectButton.onClick.AddListener(() => Select());
    }

    public void Select()
    {
        if (_isSelect) return;

        _isSelect = true;

        ui_selectImage.gameObject.SetActive(_isSelect);
        ui_unSelectImage.gameObject.SetActive(!_isSelect);

        onSelect?.Invoke(this);
    }

    public void UnSelect()
    {
        _isSelect = false;

        ui_selectImage.gameObject.SetActive(_isSelect);
        ui_unSelectImage.gameObject.SetActive(!_isSelect);
    }
}