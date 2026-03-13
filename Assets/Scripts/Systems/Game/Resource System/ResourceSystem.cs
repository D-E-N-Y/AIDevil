using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpawnResoure))]
public class ResourceSystem : MonoBehaviour 
{
    private SpawnResoure _spawnResoure;
    public SpawnResoure SpawnResoure => _spawnResoure;

    public void Initialize(IReadOnlyList<ResourceType> resources, LandSystem landSystem)
    {
        _spawnResoure = GetComponent<SpawnResoure>();
        _spawnResoure.Initialize(resources, landSystem);
    }
}