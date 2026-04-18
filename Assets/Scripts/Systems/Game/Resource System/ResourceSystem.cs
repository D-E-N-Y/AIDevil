using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpawnResoure))]
public class ResourceSystem : MonoBehaviour 
{
    private SpawnResoure _spawnResoure;
    public SpawnResoure SpawnResoure => _spawnResoure;

    public void Initialize(IReadOnlyList<ResourceType> resources, LandSystem landSystem, WorldPickupSystem worldPickupSystem, UI_HintController ui_hintController)
    {
        _spawnResoure = GetComponent<SpawnResoure>();
        _spawnResoure.Initialize(resources, landSystem, worldPickupSystem, ui_hintController);
    }
}