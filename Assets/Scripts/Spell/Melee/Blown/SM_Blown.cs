using System.Collections;
using UnityEngine;

public class SM_Blown : SpellMelee
{
    [SerializeField, Range(0.1f, 2f)] float timeAttacking;

    protected override IEnumerator Attacking()
    {
        IsAttacking = true;

        if(_unitFaction == UnitFaction.Enemy) yield return Cooldown();

        yield return Attack();

        if(_unitFaction == UnitFaction.Player) yield return Cooldown();

        attacking = null;
        IsAttacking = false;
    }

    protected override IEnumerator Attack()
    {
        meleeWeapon.StartAttack();
        yield return new WaitForSeconds(timeAttacking);
        meleeWeapon.FinishAttack();
    }
}