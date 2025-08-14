using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class SpellRange : Spell
{
    [SerializeField, Range(1f, 100f)] protected float attackRadius;
    [SerializeField] protected Projectile projectile;
    protected List<Projectile> projectiles;

    private SphereCollider triggerCollider;

    protected List<IHealth> units;

    public override void Initialize()
    {
        projectiles = new List<Projectile>();
        units = new List<IHealth>();

        triggerCollider = GetComponent<SphereCollider>();
        triggerCollider.radius = attackRadius;
    }

    public override void Cast()
    {
        Projectile _projectile = GetAvaliableProjectile();

        if (_projectile == null)
        {
            _projectile = Instantiate(projectile);
            projectiles.Add(_projectile);
        }

        _projectile.Initialize(transform.position);
        _projectile.Fire(targetLayer, GetNearbyEnemyPosition());
    }

    private Projectile GetAvaliableProjectile()
    {
        Projectile _avaliableProjectile = projectiles
            .Where(x => x.isAvaliable)
            .FirstOrDefault();

        return _avaliableProjectile;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsCorrentTarget(other.gameObject) &&
            other.gameObject.TryGetComponent(out MonoBehaviour comp) &&
            comp is IHealth _unit &&
            !units.Contains(_unit))
        {

            units.Add(_unit);
            _unit.onDead += RemoveTargetUnit;

            if (attacking == null)
            {
                attacking = StartCoroutine(nameof(Attacking));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsCorrentTarget(other.gameObject) &&
            other.gameObject.TryGetComponent(out MonoBehaviour comp) &&
            comp is IHealth _unit &&
            units.Contains(_unit))
        {
            RemoveTargetUnit(_unit);
        }
    }

    private void RemoveTargetUnit(IHealth _unit)
    {
        units.Remove(_unit);
        _unit.onDead -= RemoveTargetUnit;
    }

    protected Vector3 GetNearbyEnemyPosition()
    {
        Vector3 _nearbyEnemyPosition = units
            .OrderBy(x => Vector3.Distance(transform.position, ((MonoBehaviour)x).transform.position))
            .Select(x => ((MonoBehaviour)x).transform.position)
            .FirstOrDefault();

        return _nearbyEnemyPosition;
    }

    private IEnumerator Attacking()
    {
        while (units.Count > 0)
        {
            Cast();

            yield return new WaitForSeconds(cooldown);
        }

        attacking = null;
    }
}