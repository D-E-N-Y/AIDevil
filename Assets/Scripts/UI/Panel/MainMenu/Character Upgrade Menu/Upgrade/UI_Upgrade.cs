using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class UI_Upgrade : UI_Panel 
{
    public event Action<UI_Upgrade> OnSelect;

    // temporaly
    [Header("Data (Temporaly)")]
    [SerializeField] private Upgrade _upgrade;
    public Upgrade Upgrade => _upgrade;

    [Header("UI Status")]
    [SerializeField] private Image ui_iconImage;
    [SerializeField] private Image ui_lockImage;
    [SerializeField] private Image ui_purchasedImage;

    [Header("Button")]
    [SerializeField] private Button ui_selectButton;
    
    [Header("Selection")]
    [SerializeField] private Image ui_selectImage;
    [SerializeField] private Image ui_unSelectImage;

    private bool _isLock;
    public bool IsLock => _isLock;

    private bool _isPurchased;
    public bool IsPurchased => _isPurchased;

    private bool _isSelect;
    public bool IsSelect => _isSelect;

    private string _upgrade_id;
    public string Upgrade_ID => _upgrade_id;

    public void Initialize()
    {
        ui_selectButton.onClick.RemoveAllListeners();
        ui_selectButton.onClick.AddListener(() => Select());

        if (_upgrade != null)
        {
            SetUpgrade(_upgrade);
        }

        UnSelect();
    }

    public void SetUpgrade(Upgrade upgrade)
    {
        _upgrade_id = upgrade.ID;

        // ui_icon.sprite
    }

    public void Select()
    {
        _isSelect = true;

        ui_selectImage.gameObject.SetActive(_isSelect);
        ui_unSelectImage.gameObject.SetActive(!_isSelect);

        OnSelect?.Invoke(this);
    }

    public void UnSelect()
    {
        _isSelect = false;

        ui_selectImage.gameObject.SetActive(_isSelect);
        ui_unSelectImage.gameObject.SetActive(!_isSelect);
    }

    public void SetStatus(bool isLock, bool isPurchased)
    {
        _isLock = isLock;
        _isPurchased = isPurchased;

        ui_iconImage.gameObject.SetActive(!_isLock && !_isPurchased);
        ui_lockImage.gameObject.SetActive(_isLock && !isPurchased);
        ui_purchasedImage.gameObject.SetActive(!_isLock && _isPurchased);
    }
}