using System.Collections;
using UnityEngine;

public class SM_MonoEdgeDagger : SpellMelee
{
    protected override IEnumerator Attack()
    {
        _weapon.SetParameters(_damageModifier, _criticalDamageChance, _criticalDamageModifier, _areaModifier);
        
        Vector3 target = transform.position + _spellContext.Movement.Direction * 5f;
        _weapon.PrepareAttack(transform, target);

        _weapon.StartAttack();
        yield return new WaitForSeconds(_weapon.TimeToLive);
        _weapon.FinishAttack();
    }
}