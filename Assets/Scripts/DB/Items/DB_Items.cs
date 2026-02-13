using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DataBase/Items/Main")]
public class DB_Items : ScriptableObject
{
    [SerializeField] private List<DB_ItemsByType> _types; 

    [SerializeField] private RarityChances _rarityChances;
    // public RarityChances RarityChances => _rarityChances;

    public Item GetRandomItem()
    {
        int _randomType = Random.Range(0, _types.Count);
        return _types[_randomType].GetRandomItem();
    }

    public Item GetRandomItemByRarity(ItemRarity rarity)
    {
        int _randomType = Random.Range(0, _types.Count);
        return _types[_randomType].GetRandomItemByRarity(rarity);
    }

    public Item GetRandomItemByRarityChance()
    {
        float _chance = Random.Range(0.00f, 1.00f);
        ItemRarity _rarity = _rarityChances.GetRarityByChance(_chance);

        return GetRandomItemByRarity(_rarity);
    }
}