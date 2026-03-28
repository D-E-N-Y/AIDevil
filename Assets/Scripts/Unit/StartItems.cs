using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Start Items", menuName = "Item/StartItems")]
public class StartItems : ScriptableObject 
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