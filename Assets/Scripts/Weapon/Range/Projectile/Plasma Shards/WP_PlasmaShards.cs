using UnityEngine;

public class WP_PlasmaShards : Projectile 
{
    protected override void Move()
    {
        transform.position += transform.forward * moveSpeed * Time.fixedDeltaTime;
    }
}