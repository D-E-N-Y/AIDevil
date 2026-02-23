using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RarityColors", menuName = "Item/RarityColors", order = 0)]
public class RarityColors : ScriptableObject 
{
    [System.Serializable]
    private struct RarityColor
    {
        public ItemRarity rarity;
        public Color color;
    }
    
    [SerializeField] private List<RarityColor> rarityColors;

    public Color GetColor(ItemRarity rarity)
    {
        foreach (var rarityColor in rarityColors)
        {
            if (rarityColor.rarity == rarity)
            {
                return rarityColor.color;
            }
        }
        return Color.white;
    }
}