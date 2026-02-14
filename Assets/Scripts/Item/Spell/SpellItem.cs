using UnityEngine;

[CreateAssetMenu(fileName = "New Spell Item", menuName = "Item/Spell Item")]
public class SpellItem : Item
{
    [SerializeField] private Spell _spell;
    public Spell Spell => _spell;

    public override ItemType ItemType => ItemType.Spell;

    public override void Apply(ItemContext context)
    {
        context.SpellController.AddSpell(_spell);
    }

    public override void Remove(ItemContext context)
    {
        context.SpellController.RemoveSpell(_spell);
    }
}