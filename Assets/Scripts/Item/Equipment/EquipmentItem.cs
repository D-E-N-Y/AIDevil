using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Equipment", menuName = "Item/Equipment")]
public class EquipmentItem : Item
{
    [SerializeField] private List<StatModifier> _modifiers;
    public IReadOnlyList<StatModifier> Modifiers => _modifiers;

    public override ItemType ItemType => ItemType.Equipment;

    public override void Apply(ItemContext context)
    {
        foreach(StatModifier modifier in _modifiers)
        {
            context.Stats.ModifyStat(modifier.stat, modifier.value);
        }
    }

    public override void Remove(ItemContext context)
    {
        foreach(StatModifier modifier in _modifiers)
        {
            context.Stats.ModifyStat(modifier.stat, -modifier.value);
        }
    }

    private void OnValidate()
    {
        foreach (var mod in _modifiers)
        {
            mod.Validate();
        }
    }
}