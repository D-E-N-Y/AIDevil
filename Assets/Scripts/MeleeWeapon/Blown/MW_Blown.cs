using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class MW_Blown : MeleeWeapon
{
    [SerializeField] ParticleSystem blownEffect;
    private SphereCollider _damageCollider;

    public override void Initialize(string originLayer, float rangeAttack)
    {
        base.Initialize(originLayer, rangeAttack);
        blownEffect.Stop();

        _damageCollider = GetComponent<SphereCollider>();
        _damageCollider.radius = _rangeAttack;
    }

    public override void StartAttack()
    {
        base.StartAttack();
        blownEffect.Play();
    }
}