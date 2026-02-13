using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DataBase/Items/ByRarity")]
public class DB_ItemsByRarity : ScriptableObject
{
    [SerializeField] private ItemRarity _rarity;
    public ItemRarity Rarity => _rarity;

    [SerializeField] private List<Item> _items;
    public List<Item> Items => _items;

    public Item GetRandomItem()
    {
        return _items[Random.Range(0, _items.Count)];
    }

    public bool IsHasItems() => _items.Count > 0;
}