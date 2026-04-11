using UnityEngine;

public abstract class MeleeWeapon : Weapon
{
    protected override string WeaponType => "MeleeWeapon";

    public override void Initialize(UnitFaction unitFaction)
    {
        base.Initialize(unitFaction);
    }

    public override void PrepareAttack(Transform transform, Vector3 target)
    {
        
    }

    public override void StartAttack()
    {
        gameObject.SetActive(true);
    }

    public override void FinishAttack()
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