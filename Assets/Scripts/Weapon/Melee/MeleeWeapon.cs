using System;
using UnityEngine;

public abstract class MeleeWeapon : Weapon
{
    protected override string WeaponType => "MeleeWeapon";

    public override void Initialize(UnitFaction unitFaction)
    {
        base.Initialize(unitFaction);
    }

    public virtual void StartAttack()
    {
        gameObject.SetActive(true);
    }

    public virtual void FinishAttack()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<IDamagable>(out IDamagable damagable))
        {
            ApplyDamage(damagable.GetHealth());
        }
    }
}