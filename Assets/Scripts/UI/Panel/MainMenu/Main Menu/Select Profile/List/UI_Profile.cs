using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UI_Profile : UI_Panel 
{
    public Action<UI_Profile> onSelect;
    
    [SerializeField] private Image ui_selectImage;
    [SerializeField] private Image ui_unselectImage;

    [SerializeField] private TextMeshProUGUI ui_nameProfileText;

    private bool isSelect;

    private Profile _profile;

    public void Initialize(Profile profile)
    {
        onSelect = null;
        
        _profile = profile;

        ui_nameProfileText.text = _profile.Name;

        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(() => Select());

        UnSelect();
    }

    public void Select()
    {
        isSelect = true;

        ui_selectImage.gameObject.SetActive(isSelect);
        ui_unselectImage.gameObject.SetActive(!isSelect);
        

        onSelect?.Invoke(this);
    }

    public void UnSelect()
    {
        isSelect = false;

        ui_selectImage.gameObject.SetActive(isSelect);
        ui_unselectImage.gameObject.SetActive(!isSelect);
    }

    public Profile GetProfile() => _profile;
}