using UnityEngine;

[CreateAssetMenu(fileName = "Heal Item", menuName = "Item/Consumable/Heal")]
public class HealItem : ConsumableItem
{
    [SerializeField, Range(1, 1000)] private int healAmount;

    public override ConsumableEffect Effect => ConsumableEffect.Heal;

    public override void Apply(UnitContext context)
    {
        context.UnitHealth.Heal(healAmount);
    }
}