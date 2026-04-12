using System.Collections;
using UnityEngine;

public class SR_VXPlasmaticScatter : SpellRange 
{
    [SerializeField, Range(0.1f, 5f)] private float interval = 1f;
    [SerializeField, Range(1f, 10f)] private float distance = 5f;

    protected override IEnumerator Attack()
    {
        RotateToTarget(_targetPosition);
        
        fireEffect.Play();

        Vector3 dir = (_targetPosition - transform.position).normalized;
        Vector3 startTarget = transform.position + dir * distance;

        Vector3 step = transform.forward * interval;
        Vector3 currentTarget = startTarget - (step * ((shootCount - 1) / 2f));

        for (int i = 0; i < shootCount; i++)
        {
            Weapon _avaliableWeapon = GetAvaliableWeapons();
            _avaliableWeapon.SetParameters(_damageModifier, _criticalDamageChance, _criticalDamageModifier, _areaModifier);
            _avaliableWeapon.PrepareAttack(firePosition, currentTarget);
            _avaliableWeapon.StartAttack();

            currentTarget += step;
        }

        yield return null;
    }
}