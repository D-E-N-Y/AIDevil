using System;
using System.Collections.Generic;
using UnityEngine;

public class Trader : MonoBehaviour 
{
    public event Action OnItemsGenerated;
    
    private List<Item> _items;
    public List<Item> Items => _items;

    private DB_Items _db_items;

    public void Initilaize(GameInstance gameInstance)
    {
        _db_items = gameInstance.GetDataBase().Items;   
    }

    public void GenerateItems(int itemCount = 3)
    {
        itemCount = Math.Max(1, itemCount);
        
        _items = new List<Item>();

        for (int i = 0; i < itemCount; i++)
        {
            while(true)
            {
                Item _item = _db_items.GetRandomItemByRarityChance();

                if(!_items.Contains(_item) && _item != null)
                {
                    _items.Add(_item);

                    break;
                }
            }
        }

        OnItemsGenerated?.Invoke();
    }
}