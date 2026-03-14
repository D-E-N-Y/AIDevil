using System;
using System.Collections.Generic;
using UnityEngine;

public class Wallet
{
    public event Action OnMoneyAmountChanged;

    private Dictionary<ResourceType, int> _resources;
    public IReadOnlyDictionary<ResourceType, int> Resources => _resources;

    public Wallet()
    {
        _resources = new Dictionary<ResourceType, int>();

        foreach (ResourceType resource in Enum.GetValues(typeof(ResourceType)))
        {
            _resources[resource] = 0;
        }
    }

    public void AddResource(ResourceType resource, int amount)
    {
        amount = Mathf.Max(0, amount);
        _resources[resource] += amount;

        OnMoneyAmountChanged?.Invoke();
    }

    public void RemoveResource(ResourceType resource, int amount)
    {
        amount = Mathf.Max(0, amount);
        _resources[resource] -= amount;

        OnMoneyAmountChanged?.Invoke();
    }

    public bool HasEnoughResource(ResourceType resource, int amount)
    {
        amount = Mathf.Max(0, amount);
        return _resources[resource] >= amount;
    }
}