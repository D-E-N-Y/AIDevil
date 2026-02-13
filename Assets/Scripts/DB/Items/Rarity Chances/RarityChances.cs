using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RarityChances", menuName = "DataBase/Items/RarityChances")]
public class RarityChances : ScriptableObject 
{
    [SerializeField] private List<RarityChance> _rarityChances;

    public float GetChanceByRarity(ItemRarity rarity)
    {
        foreach(RarityChance _rarityChance in _rarityChances)
        {
            if(_rarityChance.rarity == rarity)
            {
                return _rarityChance.chance;
            }
        }

        return 0f;
    }

    public ItemRarity GetRarityByChance(float chance)
    {
        float cumulative = 0f;

        foreach(RarityChance rc in _rarityChances)
        {
            cumulative += rc.chance;
            if (chance <= cumulative)
            {
                return rc.rarity;
            }
        }

        return ItemRarity.None;
    }
}