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

    protected List<Enemy> enemies;

    public override void Initialize()
    {
        projectiles = new List<Projectile>();
        enemies = new List<Enemy>();

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
        _projectile.Fire(GetNearbyEnemyPosition());
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
        if (other.gameObject.TryGetComponent<Enemy>(out Enemy _enemy) && !enemies.Contains(_enemy))
        {
            enemies.Add(_enemy);
            _enemy.onDead += RemoveTargetEnemy;

            if (attacking == null)
            {
                attacking = StartCoroutine(nameof(Attacking));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<Enemy>(out Enemy _enemy) && enemies.Contains(_enemy))
        {
            RemoveTargetEnemy(_enemy);
        }
    }

    private void RemoveTargetEnemy(Enemy _enemy)
    {
        enemies.Remove(_enemy);
        _enemy.onDead -= RemoveTargetEnemy;
    }

    protected Vector3 GetNearbyEnemyPosition()
    {
        Vector3 _nearbyEnemyPosition = enemies
            .OrderBy(x => Vector3.Distance(transform.position, x.transform.position))
            .Select(x => x.transform.position)
            .FirstOrDefault();

        return _nearbyEnemyPosition;
    }

    private IEnumerator Attacking()
    {
        while (enemies.Count > 0)
        {
            Cast();

            yield return new WaitForSeconds(cooldown);
        }

        attacking = null;
    }
}