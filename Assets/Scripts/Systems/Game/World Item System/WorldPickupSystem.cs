using System.Collections.Generic;
using UnityEngine;

public class WorldPickupSystem : MonoBehaviour 
{
    [SerializeField] private Transform _itemContainer;
    [SerializeField] private Transform _resourceContainer;
    
    [SerializeField] private WorldItem _worldItemPrefab;
    [SerializeField] private WorldResource _worldResourcePrefab;

    private Dictionary<PickupType, List<WorldPickup>> _worldPickups;

    public void Initialize()
    {
        _worldPickups = new Dictionary<PickupType, List<WorldPickup>>();
        foreach (PickupType type in System.Enum.GetValues(typeof(PickupType)))
        {
            _worldPickups[type] = new List<WorldPickup>();
        }
    }

    public void SpawnItem(Item item, Vector3 position, int amount = 1)
    {
        WorldItem worldItem = GetAwaliableWorldItem();
        
        if (worldItem != null)
        {
            worldItem.transform.position = position;
        }
        else
        {
            worldItem = Instantiate(_worldItemPrefab, position, Quaternion.identity, _itemContainer);
        }
        
        worldItem.Initialize(item, amount);
        _worldPickups[PickupType.Item].Add(worldItem);
    }

    private WorldItem GetAwaliableWorldItem()
    {
        foreach (WorldItem item in _worldPickups[PickupType.Item])
        {
            if (item.IsPickedUp)
            {
                return item;
            }
        }
        return null;
    }

    public void SpawnResource(ResourceType resource, Vector3 position, int amount = 1)
    {
        WorldResource worldResource = GetAwaliableWorldResource();
        
        if (worldResource != null)
        {
            worldResource.transform.position = position;
        }
        else
        {
            worldResource = Instantiate(_worldResourcePrefab, position, Quaternion.identity, _resourceContainer);
        }
        
        worldResource.Initialize(resource, amount);
        _worldPickups[PickupType.Resource].Add(worldResource);
    }

    private WorldResource GetAwaliableWorldResource()
    {
        foreach (WorldResource resource in _worldPickups[PickupType.Resource])
        {
            if (resource.IsPickedUp)
            {
                return resource;
            }
        }
        return null;
    }
}