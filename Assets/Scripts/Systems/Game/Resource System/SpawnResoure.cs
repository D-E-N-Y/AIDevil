using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnResoure : MonoBehaviour 
{
    public event Action<HintType, Vector3, Action<Action>, Action<Action>> onStartHint;
    
    [SerializeField] private Transform _resourceContainer;
    [SerializeField] private List<Resource> _resourcePrefabs;
    private List<Resource> _avaliableResourcePrefabs;
    private IReadOnlyList<ResourceType> _resources;
    
    private Dictionary<ResourceType, List<Resource>> _resourceByType;

    private Coroutine _spawninResources;

    private bool _isSpawningResource;
    public bool IsSpawningResource => _isSpawningResource;

    [SerializeField, Range(1, 1000)] private float _timeToSpawn;
    [SerializeField, Range(1, 1000)] private float _spread;

    private float _timer;

    private LandSystem _landSystem;
    private WorldPickupSystem _worldPickupSystem;

    public void Initialize(IReadOnlyList<ResourceType> resources, LandSystem landSystem, WorldPickupSystem worldPickupSystem, UI_HintController ui_hintController)
    {
        _resources = resources;
        SetAvaliableReources();

        _landSystem = landSystem;
        _worldPickupSystem = worldPickupSystem;

        onStartHint += ui_hintController.ShowHint;
    }

    private void SetAvaliableReources()
    {
        _avaliableResourcePrefabs = new List<Resource>();
        _resourceByType = new Dictionary<ResourceType, List<Resource>>();
        
        foreach (Resource resource in _resourcePrefabs)
        {
            foreach (ResourceType type in _resources)
            {
                if (resource.Type == type)
                {
                    _avaliableResourcePrefabs.Add(resource);

                    _resourceByType[type] = new List<Resource>();
                }
            }
        }
    }

    public void StartSpawn()
    {
        if (_spawninResources != null)
        {
            StopSpawn();
        }

        _spawninResources = StartCoroutine(Spawning());
        _isSpawningResource = true;
    }

    public void StopSpawn()
    {
        if (_spawninResources != null)
        {
            StopCoroutine(_spawninResources);
            _spawninResources = null;
        }

        _isSpawningResource = false;
    }

    private IEnumerator Spawning()
    {
        yield return null;

        while (_isSpawningResource)
        {
            yield return new WaitForSeconds(GetTimeToSpawn());

            Spawn();
        }
    }

    private float GetTimeToSpawn()
    {
        float min = Math.Max(1, _timeToSpawn - _spread);
        float max = _timeToSpawn + _spread;

        float finalAmount = UnityEngine.Random.Range(min, max);

        return finalAmount;
    }

    private void Spawn()
    {
        Resource randomResource = GetRandomResource();
        Vector3 spawnPosition = _landSystem.GetValidRandomPosition();
        Quaternion spawnRotation = Quaternion.identity;

        Resource resource = GetAvaliableResource(randomResource.Type);

        if (resource == null)
        {
            resource = Instantiate(randomResource, spawnPosition, spawnRotation);
            resource.transform.SetParent(_resourceContainer);
            _resourceByType[randomResource.Type].Add(resource);
        }
        else
        {
            resource.transform.position = spawnPosition;
            resource.transform.rotation = spawnRotation;
        }
        resource.Initialize();

        resource.OnDead -= HandleResourceDead;
        resource.OnDead += HandleResourceDead;

        onStartHint?.Invoke(
            HintType.Resource, 
            spawnPosition, 
            h => resource.IHealth.OnDead += h,
            h => resource.IHealth.OnDead -= h
        );
    }

    private void HandleResourceDead(IDamagable damagable)
    {
        Resource resource = (Resource)damagable;
        _worldPickupSystem.SpawnResource(resource.Type, resource.transform.position, resource.Amount);
    }

    private Resource GetAvaliableResource(ResourceType type)
    {
        foreach (Resource resource in _resourceByType[type])
        {
            if (resource.IsDead)
            {
                return resource;
            }
        }
        return null;
    }

    private Resource GetRandomResource()
    {
        int randomIndex = UnityEngine.Random.Range(0, _avaliableResourcePrefabs.Count);   
        return _avaliableResourcePrefabs[randomIndex];
    }
}