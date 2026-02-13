using System;
using UnityEngine;

[Serializable]
public struct RarityChance
{
    [SerializeField] public ItemRarity rarity;
    [SerializeField, Range(0f, 1f)] public float chance;
}