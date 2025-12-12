using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UI_SessionResult : UI_Panel
{
    public Action<UI_SessionResult> onSelect;
    
    [SerializeField] private Image ui_selectImage;
    private bool _isSelect;

    [SerializeField] private TextMeshProUGUI ui_nameSessionText;
    private Color _selectColorText;
    private Color _defaultColorText;

    private SSesionResult _result;

    
    public void Initialize(SSesionResult result)
    {
        onSelect = null;
        
        _result = result;

        ui_nameSessionText.text = _result.name;

        _selectColorText = new Color(255f / 255f, 243f / 255f, 208f / 255f);
        _defaultColorText = new Color(32f / 255f, 18f / 255f, 6f / 255f);

        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(() => Select());

        UnSelect();
    }

    public void Select()
    {
        _isSelect = true;
        
        ui_selectImage.gameObject.SetActive(_isSelect);
        
        ui_nameSessionText.color = _selectColorText;
        ui_nameSessionText.fontStyle = FontStyles.Bold;

        onSelect?.Invoke(this);
    }

    public void UnSelect()
    {
        _isSelect = false;
        
        ui_selectImage.gameObject.SetActive(_isSelect);
        
        ui_nameSessionText.color = _defaultColorText;
        ui_nameSessionText.fontStyle = FontStyles.Normal;
    }

    public SSesionResult GetSesionResult() => _result;
}
