using UnityEngine;

public class WP_RapidLaserStream : Projectile 
{
    [SerializeField, Range(0f, 90f)] private float spreadAngle = 5f; 

    protected override void Move()
    {
        transform.position += transform.forward * moveSpeed * Time.fixedDeltaTime;
    }

    protected override void RotateToTarget(Vector3 targetPosition)
    {
        base.RotateToTarget(targetPosition);
        ApplySpread();
    }

    private void ApplySpread()
    {
        float x = Random.Range(-spreadAngle, spreadAngle);
        float y = Random.Range(-spreadAngle, spreadAngle);
        float z = 0f;

        transform.rotation *= Quaternion.Euler(x, y, z);
    }
}