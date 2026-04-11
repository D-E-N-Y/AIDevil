using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class SpellRange : Spell
{
    protected List<Weapon> _weapons;

    [SerializeField] protected Sensor _sensor;

    [SerializeField] protected Transform firePosition;

    [SerializeField] protected ParticleSystem fireEffect;

    [SerializeField, Range(1, 10)] protected int shootCount = 1;

    protected Vector3 _targetPosition;

    public bool IsCanAttack { get; protected set; }

    public override void Initialize(SpellContext spellContext)
    {
        base.Initialize(spellContext);

        if (_weapons == null)
        {
            _weapons = new List<Weapon>();
        }

        _sensor.Initialize(_spellContext.UnitFaction, rangeAttack);

        fireEffect.Stop();
    }

    public override void Cast()
    {
        if (attacking == null && IsCanAttack)
        {
            attacking = StartCoroutine(nameof(Attacking));
        }
    }

    protected Weapon GetAvaliableWeapons()
    {
        Weapon _avaliableProjectile = _weapons
            .Where(x => x.isAvaliable)
            .FirstOrDefault();

        if (_avaliableProjectile == null)
        {
            return CreateNewProjectile();
        }

        return _avaliableProjectile;
    }

    protected Weapon CreateNewProjectile()
    {
        Weapon newWeapon = Instantiate(_weapon, firePosition.position, Quaternion.identity);
        _weapons.Add(newWeapon);
        newWeapon.Initialize(_spellContext.UnitFaction);

        newWeapon.onSuccessfulAttack += () => onSuccessfulAttack?.Invoke();

        return newWeapon;
    }

    protected override IEnumerator Cooldown()
    {
        RotateToTarget(_targetPosition);

        return base.Cooldown();
    }

    protected override IEnumerator Attacking() 
    {
        IsAttacking = true;
        onStartAttack?.Invoke();

        while (IsCanAttack)
        {
            _targetPosition = _sensor.GetNearestTarget().position;

            if (_spellContext.UnitFaction == UnitFaction.Enemy) yield return Cooldown();

            yield return Attack();
            onAttack?.Invoke();

            if (IsMultiattack())
            {
                yield return new WaitForSeconds(0.1f);

                yield return Attack();
                onAttack?.Invoke();
            }

            if (_spellContext.UnitFaction == UnitFaction.Player) yield return Cooldown();
        }

        attacking = null;
        IsAttacking = false;
        onStopAttack?.Invoke();
    }

    protected override IEnumerator Attack()
    {
        RotateToTarget(_targetPosition);
        
        for (int i = 0; i < shootCount; i++)
        {
            fireEffect.Play();

            Weapon _avaliableWeapon = GetAvaliableWeapons();
            _avaliableWeapon.SetParameters(_damageModifier, _criticalDamageChance, _criticalDamageModifier, _areaModifier);
            _avaliableWeapon.PrepareAttack(firePosition, _targetPosition);
            _avaliableWeapon.StartAttack();
            
            yield return new WaitForSeconds(0.05f);
        }

        yield return null;
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

        if (IsCanAttack && _spellContext.UnitFaction == UnitFaction.Player)
        {
            Cast();
        }
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