using System;
using UnityEngine;

[Serializable]
public class PlayerCharacterStats : UnitStats 
{
    [SerializeField, Range(0f, 1f)] public float pickUpRangeModifier;
    [SerializeField, Range(0f, 1f)] public float moneyModifier;
}