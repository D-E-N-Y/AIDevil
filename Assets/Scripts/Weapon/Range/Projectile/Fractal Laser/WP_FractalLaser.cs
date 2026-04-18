using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WP_FractalLaser : Projectile 
{
    [SerializeField] private Weapon weapon;
    [SerializeField, Range(0, 10)] private int countWeapons; 
    private List<Weapon> _weapons;

    public override void Initialize(UnitFaction unitFaction)
    {
        base.Initialize(unitFaction);

        _weapons = new List<Weapon>();
    }

    protected override void Move()
    {
        transform.position += transform.forward * moveSpeed * Time.fixedDeltaTime;
    }

    protected override void Hit(Collider collider)
    {
        if(!isCanAttack) return;
        if (_ignoreTargets.Contains(collider)) return;

        if (collider.gameObject.TryGetComponent<IDamagable>(out IDamagable damagable))
        {
            ApplyDamage(damagable.IHealth);

            if (_currentPenetrationCount >= maxPenetrationCount)
            {
                _ignoreTargets.Add(collider);
                
                isCanAttack = false;
                FinishAttack();
            }
            else
            {
                Penetration();
                isCanAttack = true;
            }
        }
    }

    public override void FinishAttack()
    {
        FractalAttack();
        
        base.FinishAttack();
    }

    private void FractalAttack()
    {
        float angle = 360f / countWeapons;

        Vector3 currentTarget = transform.position + transform.forward * 10f;
        Vector3 currentDir = (currentTarget - transform.position).normalized;

        for (int i = 0; i < countWeapons; i++)
        {
            Weapon weapon = GetAvaliableWeapon();

            weapon.SetParameters(_damageModifier, _criticalDamageChance, _criticalDamageModifier, _areaModifier);
            weapon.PrepareAttack(transform, currentTarget);
            weapon.SetIgnoreTargets(_ignoreTargets.ToHashSet());
            weapon.StartAttack();

            currentDir = Quaternion.AngleAxis(angle, transform.up) * currentDir;
            currentTarget = transform.position + currentDir * 10f;
        }
    }

    protected Weapon GetAvaliableWeapon()
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
        Weapon newWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
        _weapons.Add(newWeapon);
        newWeapon.Initialize(_unitFaction);

        return newWeapon;
    }
}