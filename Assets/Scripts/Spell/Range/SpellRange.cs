using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class SpellRange : Spell
{
    protected Projectile _projectile;
    protected List<Projectile> _projectiles;

    [SerializeField] protected Sensor _sensor;

    protected Vector3 _targetPosition;

    public bool IsCanAttack { get; protected set; }

    public override void Initialize(UnitFaction unitFaction, UnitStats stats)
    {
        base.Initialize(unitFaction, stats);

        _projectile = _weapon as Projectile;
        _projectiles = new List<Projectile>();

        _sensor.Initialize(_unitFaction, rangeAttack);
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
        Projectile _avaliableProjectile = _projectiles
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
            _targetPosition = _sensor.GetNearestTarget().position;

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
        _sensor.OnUnitEnter += (_) => SetValiableAttack();
        _sensor.OnUnitExit += (_) => SetValiableAttack();
    }

    protected override void RemoveSubsriptions()
    {
        _sensor.OnUnitEnter -= (_) => SetValiableAttack();
        _sensor.OnUnitExit -= (_) => SetValiableAttack();
    }

    private void SetValiableAttack()
    {
        IsCanAttack = _sensor.IsHasUnits();

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