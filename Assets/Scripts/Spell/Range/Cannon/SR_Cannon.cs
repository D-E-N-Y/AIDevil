using System.Collections;
using UnityEngine;

public class SR_Cannon : SpellRange
{
    [SerializeField] private Transform firePosition;
    [SerializeField] private ParticleSystem cannonFireEffect;

    public override void Initialize(UnitFaction unitFaction)
    {
        base.Initialize(unitFaction);
        cannonFireEffect.Stop();
    }

    protected override IEnumerator Attack()
    {
        cannonFireEffect.Play();

        Projectile _projectile = GetAvaliableProjectile();

        RotateToTarget(_targetPosition);

        if (_projectile == null)
        {
            _projectile = Instantiate(projectile);
            projectiles.Add(_projectile);

            _projectile.onSuccessfulAttack += () => onSuccessfulAttack?.Invoke();
        }

        _projectile.Initialize(_unitFaction, firePosition.transform.position);
        _projectile.Fire(_targetPosition);

        yield return null;
    }

    private void RotateToTarget(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        direction.y = 0;
        direction.Normalize();

        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation * Quaternion.Euler(0, -90, 0);
    }
}