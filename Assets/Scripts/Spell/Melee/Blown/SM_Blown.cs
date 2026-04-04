using System.Collections;
using UnityEngine;

public class SM_Blown : SpellMelee
{
    [SerializeField, Range(0.1f, 2f)] float timeAttacking;

    protected override IEnumerator Attack()
    {
        _meleeWeapon.PrepareAttack(_damageModifier, _criticalDamageChance, _criticalDamageModifier, _areaModifier);

        _meleeWeapon.StartAttack();
        yield return new WaitForSeconds(timeAttacking);
        _meleeWeapon.FinishAttack();
    }
}