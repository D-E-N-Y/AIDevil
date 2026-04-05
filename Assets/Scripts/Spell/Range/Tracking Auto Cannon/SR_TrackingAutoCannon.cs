using System.Collections;
using UnityEngine;

public class SR_TrackingAutoCannon : SpellRange
{
    [SerializeField, Range(1, 10)] private int shootCount = 3;

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
        
        for (int i = 0; i < shootCount; i++)
        {
            Projectile _avaliableProjectile = GetAvaliableProjectile();

            _avaliableProjectile.PrepareAttack(_damageModifier, _criticalDamageChance, _criticalDamageModifier, _areaModifier);
            _avaliableProjectile.SetToFire(firePosition.position);
            _avaliableProjectile.Fire(_targetPosition);
            
            yield return new WaitForSeconds(0.05f);
        }
    }
}