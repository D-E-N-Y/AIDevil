using System;
using UnityEngine;

public class WP_RailShot : Projectile
{
    [SerializeField, Range(0f, 50f)] private float lessDamage = 10; 
    
    protected override void Move()
    {
        transform.position += transform.forward * moveSpeed * Time.fixedDeltaTime;
    }

    protected override void Penetration()
    {
        base.Penetration();

        _damageModifier -= lessDamage / 100f;
    }
}