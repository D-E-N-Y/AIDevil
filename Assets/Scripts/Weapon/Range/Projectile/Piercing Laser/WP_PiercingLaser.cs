using UnityEngine;

public class WP_PiercingLaser : Projectile 
{
    protected override void Move()
    {
        transform.position += transform.forward * moveSpeed * Time.fixedDeltaTime;
    }
}