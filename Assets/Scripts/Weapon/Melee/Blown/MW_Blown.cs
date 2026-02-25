using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class MW_Blown : MeleeWeapon
{
    [SerializeField, Range(0.1f, 10f)] private float _radiusAttack;
    
    [SerializeField] ParticleSystem blownEffect;
    private SphereCollider _damageCollider;

    public override void Initialize(UnitFaction unitFaction)
    {
        base.Initialize(unitFaction);
        blownEffect.Stop();

        _damageCollider = GetComponent<SphereCollider>();
        _damageCollider.radius = _radiusAttack;
    }

    public override void StartAttack()
    {
        base.StartAttack();
        blownEffect.Play();
    }
}