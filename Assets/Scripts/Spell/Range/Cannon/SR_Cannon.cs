using System.Collections;
using UnityEngine;

public class SR_Cannon : SpellRange
{
    [SerializeField] private Transform firePosition;
    [SerializeField] private ParticleSystem cannonFireEffect;

    public override void Initialize(UnitFaction unitFaction, UnitStats stats)
    {
        base.Initialize(unitFaction, stats);
        cannonFireEffect.Stop();
    }

    protected override IEnumerator Cooldown()
    {
        RotateToTarget(_targetPosition);

        return base.Cooldown();
    }

    protected override IEnumerator Attack()
    {
        RotateToTarget(_targetPosition);

        cannonFireEffect.Play();

        Projectile _avaliableProjectile = GetAvaliableProjectile();

        if (_avaliableProjectile == null)
        {
            _avaliableProjectile = Instantiate(_projectile);
            _projectiles.Add(_avaliableProjectile);
            _avaliableProjectile.Initialize(_unitFaction);

            _avaliableProjectile.onSuccessfulAttack += () => onSuccessfulAttack?.Invoke();
        }

        _avaliableProjectile.PrepareAttack(_damageModifier, _criticalDamageChance, _criticalDamageModifier, _areaModifier);
        _avaliableProjectile.SetToFire(firePosition.position);
        _avaliableProjectile.Fire(_targetPosition);

        yield return null;
    }

    protected void RotateToTarget(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        direction.y = 0;
        direction.Normalize();

        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation * Quaternion.Euler(0, -90, 0);
    }
}