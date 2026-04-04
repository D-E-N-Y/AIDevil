using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class MW_Dash : MeleeWeapon 
{
    [SerializeField] ParticleSystem blownEffect;
    private CapsuleCollider _damageCollider;

    public override void Initialize(UnitFaction unitFaction)
    {
        base.Initialize(unitFaction);
        blownEffect.Stop();

        _damageCollider = GetComponent<CapsuleCollider>();
    }

    public override void StartAttack()
    {
        base.StartAttack();
        blownEffect.Play();
    }
}