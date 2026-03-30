using System;
using System.Collections.Generic;
using UnityEngine;

public class Wallet
{
    public event Action OnResourceAmountChanged;

    private Dictionary<ResourceType, int> _resources;
    public IReadOnlyDictionary<ResourceType, int> Resources => _resources;

    private ResourceType[] resourceTypes;

    public Wallet()
    {
        resourceTypes = (ResourceType[])Enum.GetValues(typeof(ResourceType));
        _resources = new Dictionary<ResourceType, int>();

        foreach (ResourceType resource in resourceTypes)
        {
            _resources[resource] = 0;
        }
    }

    public Wallet(Dictionary<ResourceType, int> resources)
    {
        resourceTypes = (ResourceType[])Enum.GetValues(typeof(ResourceType));
        _resources = new Dictionary<ResourceType, int>();

        foreach (ResourceType resource in resourceTypes)
        {
            int amount = resources.ContainsKey(resource) ? resources[resource] : 0;
            _resources[resource] = amount;
        }
    }

    public Wallet(IReadOnlyList<Cost> costs)
    {
        resourceTypes = (ResourceType[])Enum.GetValues(typeof(ResourceType));
        _resources = new Dictionary<ResourceType, int>();

        foreach (ResourceType resource in resourceTypes)
        {
            _resources[resource] = 0;
        }

        foreach (Cost cost in costs)
        {
            AddResource(cost.resource, cost.amount);
        }
    }

    public void AddResource(ResourceType resource, int amount)
    {
        amount = Mathf.Max(0, amount);
        _resources[resource] += amount;

        Debug.Log($"Add {resource} {amount} to Wallet");

        OnResourceAmountChanged?.Invoke();
    }

    public void AddResources(Dictionary<ResourceType, int> resources)
    {
        Debug.Log("Add resources");
        
        foreach (ResourceType resource in resourceTypes)
        {
            if (resources.ContainsKey(resource))
            {
                AddResource(resource, resources[resource]);
            }
        }
    }

    public void RemoveResource(ResourceType resource, int amount)
    {
        amount = Mathf.Max(0, amount);
        _resources[resource] -= amount;

        OnResourceAmountChanged?.Invoke();
    }

    public void RemoveResources(IReadOnlyList<Cost> costs)
    {
        foreach (Cost cost in costs)
        {
            RemoveResource(cost.resource, cost.amount);
        }
    }

    public bool HasEnoughResource(ResourceType resource, int amount)
    {
        amount = Mathf.Max(0, amount);
        return _resources[resource] >= amount;
    }

    public bool HasEnoughResources(IReadOnlyList<Cost> costs)
    {
        foreach (Cost cost in costs)
        {
            if (!HasEnoughResource(cost.resource, cost.amount))
            {
                return false;
            }
        }

        return true;
    }

    public bool HasResources(ResourceType resource)
    {
        if (_resources.ContainsKey(resource))
        {
            return _resources[resource] > 0;
        }
        else
        {
            return false;
        }
    }

    public int GetAmountByResource(ResourceType resource)
    {
        return _resources[resource];
    }
}