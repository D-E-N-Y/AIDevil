using System;
using System.Collections.Generic;
using UnityEngine;

public class UI_Wallet : UI_Panel 
{
    [SerializeField] private UI_ResourceValue ui_resourceValuePrefab;
    private Dictionary<ResourceType, UI_ResourceValue> ui_resources;

    [SerializeField] private RectTransform _containerUIResources;

    private Wallet _wallet;

    private ResourceType[] _types;

    public void Initialize(Wallet wallet)
    {
        _wallet = wallet;
        
        DisableObjectInContainer();
        InitializeUIResources();
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

        foreach (ResourceType resource in _types)
        {
            UI_ResourceValue ui_resourceValue = Instantiate(ui_resourceValuePrefab, _containerUIResources);
            ui_resourceValue.Initialize(resource);

            ui_resources[resource] = ui_resourceValue;
        }
    }

    public void UpdateData()
    {
        foreach (ResourceType resource in _types)
        {
            ui_resources[resource].SetValue(_wallet.GetAmountByResource(resource));
        }
    }

    private void OnEnable()
    {
        UpdateData();
    }
}