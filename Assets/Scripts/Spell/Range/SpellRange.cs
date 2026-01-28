using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class SpellRange : Spell
{
    [SerializeField] protected Projectile projectile;
    protected List<Projectile> projectiles;

    [SerializeField] protected Sensor sensor;

    protected Vector3 _targetPosition;

    public bool IsCanAttack { get; protected set; }

    public override void Initialize(UnitFaction unitFaction, UnitStats stats)
    {
        base.Initialize(unitFaction, stats);

        projectiles = new List<Projectile>();

        RemoveSubsriptions();
        sensor.Initialize(_unitFaction, rangeAttack);
        SetSubsriptions();
    }

    public override void Cast()
    {
        if (attacking == null && IsCanAttack)
        {
            attacking = StartCoroutine(nameof(Attacking));
        }
    }

    protected Projectile GetAvaliableProjectile()
    {
        Projectile _avaliableProjectile = projectiles
            .Where(x => x.isAvaliable)
            .FirstOrDefault();

        return _avaliableProjectile;
    }

    protected override IEnumerator Attacking()
    {
        IsAttacking = true;
        onStartAttack?.Invoke();

        while (IsCanAttack)
        {
            _targetPosition = sensor.GetNearestTarget().position;

            if (_unitFaction == UnitFaction.Enemy) yield return Cooldown();

            yield return Attack();
            onAttack?.Invoke();

            if (_unitFaction == UnitFaction.Player) yield return Cooldown();
        }

        attacking = null;
        IsAttacking = false;
        onStopAttack?.Invoke();
    }

    protected override void SetSubsriptions()
    {
        sensor.OnUnitEnter += (_) => SetValiableAttack();
        sensor.OnUnitExit += (_) => SetValiableAttack();

        _stats.OnStatChanged += UpdateStats;
    }

    protected override void RemoveSubsriptions()
    {
        sensor.OnUnitEnter -= (_) => SetValiableAttack();
        sensor.OnUnitExit -= (_) => SetValiableAttack();

        _stats.OnStatChanged -= UpdateStats;
    }

    private void SetValiableAttack()
    {
        IsCanAttack = sensor.IsHasUnits();

        if (IsCanAttack && _unitFaction == UnitFaction.Player)
        {
            Cast();
        }
    }

    // protected void RotateToTarget(Vector3 targetPosition)
    // {
    //     Vector3 direction = targetPosition - transform.position;
    //     direction.y = 0;
    //     direction.Normalize();

    //     Quaternion rotation = Quaternion.LookRotation(direction);
    //     transform.rotation = rotation * Quaternion.Euler(0, -90, 0);
    // }
}