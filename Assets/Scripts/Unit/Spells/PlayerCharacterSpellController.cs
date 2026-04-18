using System;
using System.Collections.Generic;

public class PlayerCharacterSpellController : ISpellController
{
    public event Action updateSpells;
    
    private List<Spell> _spells;
    private List<SpellMelee> _meleeSpells;
    private List<SpellRange> _rangeSpells;

    protected SpellContainer _spellContainer;

    private SpellContext _spellContext;

    public PlayerCharacterSpellController(SpellContext spellContext, SpellContainer spellContainer)
    {
        _spellContext = spellContext;
        _spellContainer = spellContainer;

        spellContainer.Initialize();

        _spells = new List<Spell>();
        _meleeSpells = new List<SpellMelee>();
        _rangeSpells = new List<SpellRange>();
    }

    private void OrderBySpell(Spell spell)
    {
        spell.Initialize(_spellContext);
        
        if (spell is SpellMelee _meleeSpell)
        {
            _meleeSpells.Add(_meleeSpell);
        }
        else if (spell is SpellRange _rangeSpell)
        {
            _rangeSpells.Add(_rangeSpell);
        }

        updateSpells?.Invoke();
    }

    public void AddSpell(Spell spell)
    {
        Spell newSpell = _spellContainer.CreateSpell(spell);
        _spells.Add(newSpell);

        OrderBySpell(newSpell);
    }

    public void AddSpells(List<Spell> spells)
    {
        foreach (Spell spell in spells)
        {
            AddSpell(spell);
        }
    }

    public void RemoveSpell(Spell spell)
    {
        _spells.Remove(spell);

        if(_meleeSpells.Contains((SpellMelee)spell))
        {
            _meleeSpells.Remove((SpellMelee)spell);
        }
        else if(_rangeSpells.Contains((SpellRange)spell))
        {
            _rangeSpells.Remove((SpellRange)spell);
        }

        spell.gameObject.SetActive(false);

        updateSpells?.Invoke();
    }

    public void CastRandomMeleeSpell()
    {
        _meleeSpells[UnityEngine.Random.Range(0, _meleeSpells.Count)].Cast();
    }

    public void CastRandomRangeSpell()
    {
        _rangeSpells[UnityEngine.Random.Range(0, _rangeSpells.Count)].Cast();
    }

    public List<SpellRange> GetSpellRanges() => _rangeSpells;
    public List<SpellMelee> GetSpellMelees() => _meleeSpells;
    public List<Spell> GetSpells() => _spells;
}