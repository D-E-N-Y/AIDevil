using System;
using System.Collections.Generic;
using UnityEngine;

public class UI_Cost : UI_Panel 
{
    [SerializeField] private UI_ResourceValue ui_resourceValuePrefab;
    private Dictionary<ResourceType, UI_ResourceValue> ui_resourceValues;
    private ResourceType[] _resourceTypes;

    [SerializeField] private RectTransform _containerUIResourceValues;

    public void Initialize()
    {
        HideAllObjectInContainer(_containerUIResourceValues);
        CreateUIResources();
    }

    private void HideAllObjectInContainer(RectTransform container)
    {
        for (int i = 0; i < container.childCount; i++)
        {
            container.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void CreateUIResources()
    {
        ui_resourceValues = new Dictionary<ResourceType, UI_ResourceValue>();

        _resourceTypes = (ResourceType[])Enum.GetValues(typeof(ResourceType));

        foreach (ResourceType resource in _resourceTypes)
        {
            UI_ResourceValue ui_resourceValue = Instantiate(ui_resourceValuePrefab, _containerUIResourceValues);
            ui_resourceValue.Initialize(resource);
            ui_resourceValue.Hide();

            ui_resourceValues[resource] = ui_resourceValue;
        }
    }

    public void UpdateUICost(IReadOnlyList<Cost> costs)
    {
        foreach (ResourceType resource in _resourceTypes)
        {
            ui_resourceValues[resource].Hide();
        }

        foreach (Cost cost in costs)
        {
            ui_resourceValues[cost.resource].SetValue(cost.amount);
            ui_resourceValues[cost.resource].Show();
        }
    }
}