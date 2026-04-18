using System;
using System.Collections.Generic;
using UnityEngine;

public interface ISpellController 
{
    event Action updateSpells;
    
    void AddSpell(Spell spell);
    void AddSpells(List<Spell> spells);
    void RemoveSpell(Spell spell);
    
    void CastRandomMeleeSpell();
    void CastRandomRangeSpell();

    List<SpellRange> GetSpellRanges();
    List<SpellMelee> GetSpellMelees();
    List<Spell> GetSpells();
}