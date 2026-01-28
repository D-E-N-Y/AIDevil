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

        Projectile _projectile = GetAvaliableProjectile();

        if (_projectile == null)
        {
            _projectile = Instantiate(projectile);
            projectiles.Add(_projectile);
            _projectile.Initialize(_unitFaction);

            _projectile.onSuccessfulAttack += () => onSuccessfulAttack?.Invoke();
        }

        _projectile.SetToFire(firePosition.position);
        _projectile.Fire(_targetPosition);

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