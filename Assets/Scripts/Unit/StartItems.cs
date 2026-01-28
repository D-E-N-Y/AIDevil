using System.Collections.Generic;
using UnityEngine;

public class StartItems : MonoBehaviour 
{
    [SerializeField] private List<Item> _items;

    public List<Item> GetStartItems() => _items;

    public List<Spell> GetStartSpells()
    {
        List<Spell> spells = new List<Spell>();

        foreach (var item in _items)
        {
            if (item is SpellItem spellItem)
            {
                spells.Add(spellItem.Spell);
            }
        }

        return spells;
    }
}