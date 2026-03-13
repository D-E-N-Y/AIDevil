using System;
using UnityEngine;

[Serializable, CreateAssetMenu(fileName = "Player Character Stats", menuName = "UnitStats/PlayerCharacter")]
public class PlayerCharacterStats : UnitStats 
{
    [SerializeField, Range(0f, 1f)] private float pickUpRangeModifier;
    public float PickUpRangeModifier => pickUpRangeModifier;

    [SerializeField, Range(0f, 1f)] private float moneyModifier;
    public float MoneyModifier => moneyModifier;

    public override void Initialize()
    {
        base.Initialize();

        _modifyStat.Add(StatType.PickUpRangeModifier, ModifyPickUpRangeModifier);
        _modifyStat.Add(StatType.MoneyModifier, ModifyMoneyModifier);

        _currentStats.Add(StatType.PickUpRangeModifier, PickUpRangeModifier);
        _currentStats.Add(StatType.MoneyModifier, MoneyModifier);
    }

    protected override void UpdateStarts()
    {
        base.UpdateStarts();

        _currentStats[StatType.PickUpRangeModifier] = PickUpRangeModifier;
        _currentStats[StatType.MoneyModifier] = MoneyModifier;
    }

    private void ModifyPickUpRangeModifier(float value)
    {
        float _pickUpRangeModifier = pickUpRangeModifier + value;
        pickUpRangeModifier = Mathf.Max(0.01f, pickUpRangeModifier);
    }

    private void ModifyMoneyModifier(float value)
    {
        float _moneyModifier = moneyModifier + value;
        moneyModifier = Mathf.Max(0.01f, moneyModifier);
    }
}