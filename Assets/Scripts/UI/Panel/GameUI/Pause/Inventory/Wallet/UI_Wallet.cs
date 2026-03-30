using System;
using System.Collections.Generic;
using UnityEngine;

public class UI_Wallet : UI_Panel 
{
    [SerializeField] private UI_ResourceValue ui_resourceValuePrefab;
    private Dictionary<ResourceType, UI_ResourceValue> ui_resources;

    [SerializeField] private List<ResourceType> _resourcesToShow;

    [SerializeField] private RectTransform _containerUIResources;

    private Wallet _wallet;

    private ResourceType[] _types;

    public void Initialize(Wallet wallet)
    {
        DisableObjectInContainer();
        InitializeUIResources();

        UpdateWallet(wallet);
    }

    public void UpdateWallet(Wallet wallet)
    {
        if (_wallet != null)
        {
            _wallet.OnResourceAmountChanged -= UpdateData;
        }
        
        _wallet = wallet;
        _wallet.OnResourceAmountChanged += UpdateData;

        UpdateData();
    }

    private void DisableObjectInContainer()
    {
        for (int i = 0; i < _containerUIResources.childCount; i++)
        {
            _containerUIResources.GetChild(i).gameObject.SetActive(false);;
        }
    }

    private void InitializeUIResources()
    {
        _types = (ResourceType[])Enum.GetValues(typeof(ResourceType));

        ui_resources = new Dictionary<ResourceType, UI_ResourceValue>();

        foreach (ResourceType resource in _resourcesToShow)
        {
            UI_ResourceValue ui_resourceValue = Instantiate(ui_resourceValuePrefab, _containerUIResources);
            ui_resourceValue.Initialize(resource);

            ui_resources[resource] = ui_resourceValue;
        }
    }

    public void UpdateData()
    {
        foreach (ResourceType resource in _resourcesToShow)
        {
            ui_resources[resource].SetValue(_wallet.GetAmountByResource(resource));
        }
    }
}