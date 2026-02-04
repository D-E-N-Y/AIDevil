using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DataBase/Items")]
public class DB_Items : ScriptableObject
{
    [SerializeField] private List<Item> items;
    
    public Item GetItemByName(string name)
    {
        return items.Find(item => item.Name == name);
    }

    public List<Item> GetAllItems()
    {
        return items;
    }

    public List<Item> GetItemsByNames(List<string> names)
    {
        List<Item> selectedItems = new List<Item>();
        foreach (string name in names)
        {
            Item item = GetItemByName(name);
            if (item != null)
            {
                selectedItems.Add(item);
            }
        }
        return selectedItems;
    }

    public Item GetRandomItem()
    {
        if (items.Count == 0) return null;
        int randomIndex = Random.Range(0, items.Count);
        return items[randomIndex];
    }
}