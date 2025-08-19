using System.Collections;
using UnityEngine;

public class SM_Blown : SpellMelee
{
    [SerializeField, Range(0.1f, 2f)] float timeAttacking;

    protected override IEnumerator Attacking()
    {
        meleeWeapon.StartAttack();
        yield return new WaitForSeconds(timeAttacking);
        meleeWeapon.FinishAttack();

        yield return new WaitForSeconds(cooldown - timeAttacking);

        attacking = null;
    }
}