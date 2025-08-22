using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class SpellRange : Spell
{
    [SerializeField, Range(1f, 100f)] protected float attackRadius;
    [SerializeField] protected Projectile projectile;
    protected List<Projectile> projectiles;

    [SerializeField] protected Sensor sensor;

    protected Vector3 _targetPosition;

    public bool IsCanAttack { get; protected set; }

    public override void Initialize(UnitFaction unitFaction)
    {
        attacking = null;

        _unitFaction = unitFaction;

        projectiles = new List<Projectile>();

        RemoveSubsriptions();
        sensor.Initialize(_unitFaction, attackRadius);
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

        while (IsCanAttack)
        {
            _targetPosition = sensor.GetNerbyUnitPosition();

            if (_unitFaction == UnitFaction.Enemy) yield return Cooldown();

            yield return Attack();

            if (_unitFaction == UnitFaction.Player) yield return Cooldown();
        }

        attacking = null;
        IsAttacking = false;
    }

    protected override void SetSubsriptions()
    {
        sensor.onEnterUnit += SetValiableAttack;
        sensor.onExitUnit += SetValiableAttack;
    }

    protected override void RemoveSubsriptions()
    {
        sensor.onEnterUnit -= SetValiableAttack;
        sensor.onExitUnit -= SetValiableAttack;
    }

    private void SetValiableAttack()
    {
        IsCanAttack = sensor.IsHasUnits();

        if (IsCanAttack && _unitFaction == UnitFaction.Player)
        {
            Cast();
        }
    }
}