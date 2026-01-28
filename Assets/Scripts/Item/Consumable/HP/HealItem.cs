using UnityEngine;

[CreateAssetMenu(fileName = "Heal Item", menuName = "Item/Consumable/Heal")]
public class HealItem : ConsumableItem
{
    [SerializeField, Range(1, 1000)] private int healAmount;

    public override void Apply(ItemContext context)
    {
        context.Owner.GetHealth().Heal(healAmount);
    }
}