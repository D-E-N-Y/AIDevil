using System;
using System.Collections.Generic;
using UnityEngine;

public class Trader : MonoBehaviour 
{
    public event Action OnItemsGenerated;
    
    private List<Item> _items;
    public List<Item> Items => _items;

    private DB_Items _dbItems;

    public void Initilaize(GameInstance gameInstance)
    {
        _dbItems = gameInstance.GetDataBase().Items;   
    }

    public void GenerateItems(int itemCount = 3)
    {
        itemCount = Math.Max(1, itemCount);
        
        _items = new List<Item>();

        for (int i = 0; i < itemCount; i++)
        {
            _items.Add(_dbItems.GetRandomItem());
        }

        OnItemsGenerated?.Invoke();
    }
}