using System.Collections.Generic;
using UnityEngine;

public class EnemySpellController : MonoBehaviour
{
    private List<Spell> _spells;
    private SpellContext _spellContext;

    private float _optimalAttackRange;

    [System.Serializable]
    private struct WorldSpell
    {
        public Spell spell;
        public UI_WorldSpellCooldown ui_cooldown;
    }
    [SerializeField] private List<WorldSpell> _worldSpells;

    public void Initialize(SpellContext spellContext)
    {
        _spellContext = spellContext;
        _spells = new List<Spell>();

        foreach (WorldSpell worldSpell in _worldSpells)
        {
            worldSpell.spell.Initialize(_spellContext);
            worldSpell.ui_cooldown.Initialize(worldSpell.spell);
            
            _spells.Add(worldSpell.spell);
        }

        _optimalAttackRange = CalculateOptimalAttackRange();
    }

    public void CastRandomSpell()
    {
        if (_spells.Count == 0) return;
        
        Spell spellToCast = _spells[UnityEngine.Random.Range(0, _spells.Count)];
        
        if (spellToCast != null && spellToCast.IsAttacking == false)
        {
            spellToCast.Cast();
        }
    }

    public bool IsAnySpellAttacking()
    {
        foreach (Spell spell in _spells)
        {
            if (spell.IsAttacking)
                return true;
        }
        return false;
    }

    private float CalculateOptimalAttackRange()
    {
        float optimalRange = float.MaxValue;
        foreach (Spell spell in _spells)
        {
            if (spell.RangeAttack() < optimalRange)
                optimalRange = spell.RangeAttack();
        }
        return optimalRange;
    }

    public List<Spell> GetSpells() => _spells;
    public float OptimalAttackRange => _optimalAttackRange;
}