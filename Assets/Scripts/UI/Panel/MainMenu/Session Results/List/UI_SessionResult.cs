using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UI_SessionResult : UI_Panel
{
    public Action<UI_SessionResult> onSelect;
    
    [SerializeField] private Image ui_selectImage;
    [SerializeField] private Image ui_unselectImage;
    private bool _isSelect;

    [SerializeField] private TextMeshProUGUI ui_nameSessionText;

    private SSesionResult _result;

    
    public void Initialize(SSesionResult result)
    {
        onSelect = null;
        
        _result = result;

        ui_nameSessionText.text = _result.name;

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

    public SSesionResult GetSesionResult() => _result;
}
