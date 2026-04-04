using System.Collections;
using UnityEngine;

public class SM_DashBlade : SpellMelee
{
    [SerializeField, Range(0.1f, 10f)] float dashSpeed;
    
    protected override IEnumerator Attack()
    {
        _spellContext.UnitHealth.SetInvulnerability(true);

        _meleeWeapon.StartAttack();

        _spellContext.Movement.Dash(rangeAttack, dashSpeed);

        while (_spellContext.Movement.IsDashing)
        {
            yield return null;
        }
        
        _meleeWeapon.FinishAttack();

        _spellContext.UnitHealth.SetInvulnerability(false);
    }
}