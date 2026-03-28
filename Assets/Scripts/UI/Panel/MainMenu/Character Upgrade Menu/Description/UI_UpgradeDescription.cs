using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_UpgradeDescription : UI_Panel 
{
    [Header("Base")]
    [SerializeField] private TextMeshProUGUI ui_nameText;

    [Header("Bonuses")]
    [SerializeField] private UI_Bonus ui_bonusPrefab;
    private List<UI_Bonus> ui_bonuses;

    [SerializeField] private RectTransform _containerUIBonuses;

    [Header("Cost")]
    [SerializeField] private UI_ResourceValue ui_resourceValuePrefab;
    private Dictionary<ResourceType, UI_ResourceValue> ui_resourceValues;
    private ResourceType[] _resourceTypes;

    [SerializeField] private RectTransform _containerUIResourceValues;


    // [Header("Require Upgrades")]
    // [SerializeField] private GameObject requireUpgrade;


    // [SerializeField] private Button ui_buyButton;

    public void Initialize()
    {
        HideAllObjectInContainer(_containerUIBonuses);
        CreateUIBonuses();

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

    private void CreateUIBonuses()
    {
        ui_bonuses = new List<UI_Bonus>();

        for(int i = 0; i < 20; i++)
        {
            UI_Bonus ui_bonus = Instantiate(ui_bonusPrefab, _containerUIBonuses);
            ui_bonus.Hide();

            ui_bonuses.Add(ui_bonus);
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

    public void SetInfo(Upgrade upgrade)
    {
        if (upgrade == null) return;

        ui_nameText.text = upgrade.Name;

        UpdateUIBonuses(upgrade);
        UpdateUICost(upgrade);
    }

    private void UpdateUIBonuses(Upgrade upgrade)
    {
        ui_bonuses.ForEach(x => x.Hide());
        IReadOnlyList<StatModifier> modifiers = upgrade.Modifiers;

        for (int i = 0; i < modifiers.Count; i++)
        {
            ui_bonuses[i].Initialize(modifiers[i].stat.ToString(), modifiers[i].value, ItemType.Equipment);
            ui_bonuses[i].Show();
        }
    }

    private void UpdateUICost(Upgrade upgrade)
    {
        foreach (ResourceType resource in _resourceTypes)
        {
            ui_resourceValues[resource].Hide();
        }
        
        IReadOnlyList<Cost> costs = upgrade.Cost;

        foreach (Cost cost in costs)
        {
            ui_resourceValues[cost.resource].SetValue(cost.amount);
            ui_resourceValues[cost.resource].Show();
        }
    }
}