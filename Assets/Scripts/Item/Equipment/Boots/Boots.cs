using UnityEngine;

[CreateAssetMenu(fileName = "Boots", menuName = "Item/Equipment/Boots")]
public class Boots : EquipmentItem
{
    [SerializeField, Range(0.1f, 1f)] private float speedBonus;

    public override void Apply(ItemContext context)
    {
        context.Stats.ModifyMoveSpeedModifier(speedBonus);
    }

    public override void Remove(ItemContext context)
    {
        context.Stats.ModifyMoveSpeedModifier(-speedBonus);
    }
}