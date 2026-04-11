using UnityEngine;

public abstract class RangeWeapon : Weapon 
{
    [SerializeField, Range(0, 10)] protected int maxPenetrationCount;
    protected int _currentPenetrationCount;
    
    [SerializeField, Range(1f, 10f)] protected float _timeToLive = 5f;
    protected float _timeAlive = 0f;
    public bool isAlive { get; protected set; }

    [Header("Visual")]
    [SerializeField] protected Transform mesh;
    [SerializeField] protected ParticleSystem impactEffect; 

    protected override string WeaponType => "RangeWeapon";

    public override void Initialize(UnitFaction unitFaction)
    {
        base.Initialize(unitFaction);
        isAvaliable = true;
    }

    protected virtual void RotateToTarget(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        direction.y = 0;
        direction.Normalize();

        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
    }

    public override void StartAttack()
    {
        isAlive = true;
    }

    protected virtual void Living()
    {
        if (!isAlive) return;

        _timeAlive += Time.fixedDeltaTime;
        if (_timeAlive >= _timeToLive)
        {
            isAlive = false;
            FinishAttack();
        }
    }

    protected virtual void Penetration()
    {
        _currentPenetrationCount++;
    }
}