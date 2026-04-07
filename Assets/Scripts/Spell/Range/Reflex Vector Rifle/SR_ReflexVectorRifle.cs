using System.Collections;
using UnityEngine;

public class SR_ReflexVectorRifle : SpellRange 
{
    [Header("Visual")]
    [SerializeField] private ParticleSystem fireEffect;
    
    public override void Initialize(SpellContext spellContext)
    {
        base.Initialize(spellContext);
        fireEffect.Stop();
    }

    protected override IEnumerator Attack()
    {
        RotateToTarget(_targetPosition);
        
        Projectile _avaliableProjectile = GetAvaliableProjectile();

        _avaliableProjectile.PrepareAttack(_damageModifier, _criticalDamageChance, _criticalDamageModifier, _areaModifier);
        _avaliableProjectile.SetToFire(firePosition.position);
        _avaliableProjectile.Fire(_targetPosition);
        
        yield return null;
    }
}