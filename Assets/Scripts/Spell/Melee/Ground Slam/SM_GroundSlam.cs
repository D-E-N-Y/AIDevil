using System.Collections;
using UnityEngine;

public class SM_GroundSlam : SpellMelee
{
    [SerializeField, Range(0.1f, 2f)] float timeAttacking;

    protected override IEnumerator Attack()
    {
        _weapon.SetParameters(_damageModifier, _criticalDamageChance, _criticalDamageModifier, _areaModifier);

        _weapon.StartAttack();
        yield return new WaitForSeconds(timeAttacking);
        _weapon.FinishAttack();
    }
}