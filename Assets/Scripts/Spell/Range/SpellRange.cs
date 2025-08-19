using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpellRange : Spell
{
    [SerializeField, Range(1f, 100f)] protected float attackRadius;
    [SerializeField] protected Projectile projectile;
    protected List<Projectile> projectiles;

    [SerializeField] protected Sensor sensor;

    public override void Initialize(string originLayer)
    {
        _originLayer = originLayer;

        projectiles = new List<Projectile>();

        sensor.Initialize(_originLayer, attackRadius);
        sensor.onEnterUnit += StartAttack;
    }

    public override void Cast()
    {
        Projectile _projectile = GetAvaliableProjectile();

        if (_projectile == null)
        {
            _projectile = Instantiate(projectile);
            projectiles.Add(_projectile);
        }

        _projectile.Initialize(_originLayer, transform.position);
        _projectile.Fire(sensor.GetNerbyUnitPosition());
    }

    private Projectile GetAvaliableProjectile()
    {
        Projectile _avaliableProjectile = projectiles
            .Where(x => x.isAvaliable)
            .FirstOrDefault();

        return _avaliableProjectile;
    }

    protected virtual void StartAttack()
    {
        if (attacking == null)
        {
            attacking = StartCoroutine(nameof(Attacking));
        }
    }

    private IEnumerator Attacking()
    {
        while (sensor.IsHasUnits())
        {
            Cast();

            yield return new WaitForSeconds(cooldown);
        }

        attacking = null;
    }
}