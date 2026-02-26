public abstract class ConsumableItem : Item
{
    public override ItemType Type => ItemType.Consumable;
    public abstract ConsumableEffect Effect { get; }
}