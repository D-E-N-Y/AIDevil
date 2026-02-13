using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DataBase/Items/ByType")]
public class DB_ItemsByType : ScriptableObject
{
    [SerializeField] private ItemType _type;
    public ItemType Type => _type;

    [SerializeField] private List<DB_ItemsByRarity> _rarities;

    public Item GetRandomItem()
    {
        int _randomRarity = Random.Range(0, _rarities.Count);
        return _rarities[_randomRarity].GetRandomItem();
    }

    public Item GetRandomItemByRarity(ItemRarity rarity)
    {
        foreach(DB_ItemsByRarity _db in _rarities)
        {
            if(_db.Rarity == rarity && _db.IsHasItems())
            {
                return _db.GetRandomItem();
            }
        }

        return null;
    }

    public bool IsHasItems()
    {
        foreach(DB_ItemsByRarity _db in _rarities)
        {
            if(_db.IsHasItems())
            {
                return true;
            }
        }

        return false;
    }
}