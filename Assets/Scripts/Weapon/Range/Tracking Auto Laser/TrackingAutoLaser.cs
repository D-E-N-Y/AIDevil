using UnityEngine;

public class TrackingAutoLaser : Projectile
{
    [SerializeField, Range(0.1f, 5f)] private float trackingStrength;
    
    [Header("Sensor")]
    [SerializeField] private Sensor sensor;
    [SerializeField] private float trackingRange;

    public override void Initialize(UnitFaction unitFaction)
    {
        base.Initialize(unitFaction);

        sensor.Initialize(unitFaction, trackingRange);
    }

    private Vector3 CalculateTrackingDirection()
    {
        if (!sensor.IsHasUnits())
        {
            return transform.forward;
        }

        Transform nearestTarget = sensor.GetNearestTarget();
        Vector3 directionToTarget = (nearestTarget.position - transform.position).normalized;

        return directionToTarget;
    }

    protected override void Move()
    {
        Vector3 currentDirection = CalculateTrackingDirection();
        RotatePorjectile(currentDirection);
        
        transform.position += transform.forward * moveSpeed * Time.fixedDeltaTime;
    }

    private void RotatePorjectile(Vector3 direction)
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            trackingStrength * Time.fixedDeltaTime
        );
    }
}