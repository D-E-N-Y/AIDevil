using UnityEngine;

public class TrackingAutoLaser : Projectile
{
    [SerializeField, Range(1f, 10f)] private float trackingStrength;
    
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

    private float _timeToLive = 5f;
    private float _timeAlive = 0f;

    protected override void Move()
    {
        if (_targetPosition == Vector3.zero) return;

        Vector3 currentDirection = CalculateTrackingDirection();
        
        RotatePorjectile(currentDirection);
        transform.position += currentDirection * moveSpeed * Time.fixedDeltaTime;

        _timeAlive += Time.fixedDeltaTime;
        if (_timeAlive >= _timeToLive)
        {
            _timeAlive = 0f;
            
            _targetPosition = Vector3.zero;
            mesh.gameObject.SetActive(false);

            isAvaliable = true;
            gameObject.SetActive(false);
        }
    }

    private void RotatePorjectile(Vector3 direction)
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            trackingStrength * trackingStrength * Time.fixedDeltaTime
        );
    }
}