using System.Collections;
using UnityEngine;

public class SR_Cannon : SpellRange
{
    [SerializeField] private ParticleSystem cannonFireEffect;

    public override void Initialize(SpellContext spellContext)
    {
        base.Initialize(spellContext);
        cannonFireEffect.Stop();
    }

    protected override IEnumerator Attack()
    {
        RotateToTarget(_targetPosition);

        cannonFireEffect.Play();

        Projectile _avaliableProjectile = GetAvaliableProjectile();

        _avaliableProjectile.PrepareAttack(_damageModifier, _criticalDamageChance, _criticalDamageModifier, _areaModifier);
        _avaliableProjectile.SetToFire(firePosition.position);
        _avaliableProjectile.Fire(_targetPosition);

        yield return null;
    }
}