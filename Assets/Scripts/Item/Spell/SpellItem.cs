using UnityEngine;

[CreateAssetMenu(fileName = "New Spell Item", menuName = "Item/Spell Item")]
public class SpellItem : Item
{
    [SerializeField] private Spell _spell;
    public Spell Spell => _spell;

    public override ItemType Type => ItemType.Spell;

    public override void Apply(UnitContext context)
    {
        context.SpellController.AddSpell(_spell);
    }

    public override void Remove(UnitContext context)
    {
        context.SpellController.RemoveSpell(_spell);
    }
}