using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_GameLevelDescription : UI_Panel 
{
    [SerializeField] private TextMeshProUGUI ui_wavesCountText;
    [SerializeField] private TextMeshProUGUI ui_enemyCountText;

    [SerializeField] private UI_Resource ui_resourcePrefab;
    [SerializeField] private Transform _containerUIResources;

    private Dictionary<ResourceType, UI_Resource> ui_resources;
    private ResourceType[] resourceTypes;
 
    public void Initialize()
    {
        resourceTypes = (ResourceType[])Enum.GetValues(typeof(ResourceType));
        CreateUIResources();
    }

    private void CreateUIResources()
    {
        for (int i = 0; i < _containerUIResources.childCount; i++)
        {
            _containerUIResources.GetChild(i).gameObject.SetActive(false);
        }

        ui_resources = new Dictionary<ResourceType, UI_Resource>();

        foreach (ResourceType resource in resourceTypes)
        {
            UI_Resource ui_resource = Instantiate(ui_resourcePrefab, _containerUIResources);
            ui_resource.Initialize(resource);
            ui_resources[resource] = ui_resource;
        }
    }

    public void UpdateInfo(GameLevel gameLevel)
    {
        ui_wavesCountText.text = gameLevel.WaveConfig.GetWavesCount().ToString();
        ui_enemyCountText.text = gameLevel.WaveConfig.GetEnemyCount().ToString();

        UpdateUIResources(gameLevel.Resources);
    }

    private void UpdateUIResources(IReadOnlyList<ResourceType> resources)
    {
        foreach (ResourceType resource in resourceTypes)
        {
            ui_resources[resource].Hide();
        }

        foreach (ResourceType resource in resources)
        {
            ui_resources[resource].Show();
        }
    }
}