using System.Collections;
using UnityEngine;

public abstract class SpellMelee : Spell
{
    public override void Initialize(SpellContext spellContext)
    {
        base.Initialize(spellContext);
    }

    public override void Cast()
    {
        if (attacking == null)
        {
            attacking = StartCoroutine(nameof(Attacking));
        }
    }

    protected override IEnumerator Attacking()
    {
        IsAttacking = true;

        if(_spellContext.UnitFaction == UnitFaction.Enemy) yield return Cooldown();

        _weapon.SetParameters(_damageModifier, _criticalDamageChance, _criticalDamageModifier, _areaModifier);

        yield return Attack();

        if (IsMultiattack())
        {
            yield return new WaitForSeconds(0.1f);
            yield return Attack();
        }

        if(_spellContext.UnitFaction == UnitFaction.Player) yield return Cooldown();

        attacking = null;
        IsAttacking = false;
    }
}