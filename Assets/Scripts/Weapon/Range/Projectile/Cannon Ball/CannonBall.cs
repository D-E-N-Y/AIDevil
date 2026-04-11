using UnityEngine;

public class CannonBall : Projectile
{
    protected override void Move()
    {
        transform.position += transform.forward * moveSpeed * Time.fixedDeltaTime;
    }
}