using System;
using UnityEngine;

[Serializable]
public class PlayerCharacterStats : UnitStats 
{
    [SerializeField, Range(0f, 1f)] private float pickUpRangeModifier;
    public float PickUpRangeModifier => pickUpRangeModifier;

    [SerializeField, Range(0f, 1f)] private float moneyModifier;
    public float MoneyModifier => moneyModifier;

    public void ModifyPickUpRangeModifier(float value)
    {
        float _pickUpRangeModifier = pickUpRangeModifier + value;
        pickUpRangeModifier = Mathf.Max(0.01f, pickUpRangeModifier);

        RaiseStatChanged(StatType.PickUpRangeModifier);
    }

    public void ModifyMoneyModifier(float value)
    {
        float _moneyModifier = moneyModifier + value;
        pickUpRangeModifier = Mathf.Max(0.01f, pickUpRangeModifier);

        RaiseStatChanged(StatType.MoneyModifier);
    }
}