using UnityEngine;

public class WP_LaserBolt : Projectile 
{
    protected override void Move()
    {
        transform.position += transform.forward * moveSpeed * Time.fixedDeltaTime;
    }
}