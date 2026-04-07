using System;
using System.Collections;
using UnityEngine;

public abstract class Projectile : Weapon
{
    [SerializeField, Range(1f, 10f)] protected float _timeToLive = 5f;
    protected float _timeAlive = 0f;
    
    [SerializeField] protected Transform mesh;
    [SerializeField] private ParticleSystem impactEffect;

    [SerializeField, Range(1f, 100f)] protected float moveSpeed;

    [SerializeField, Range(0, 10)] protected int maxPenetrationCount;
    protected int _currentPenetrationCount; 

    protected Vector3 _targetPosition;

    public bool isCanAttack { get; protected set; }
    public bool isAvaliable { get; protected set; }

    protected bool _isMove;
    public bool IsMove => _isMove; 

    protected override string WeaponType => "Projectile";

    public override void Initialize(UnitFaction unitFaction)
    {
        base.Initialize(unitFaction);

        isAvaliable = true;
    }

    public virtual void SetToFire(Vector3 position)
    {
        isCanAttack = true;
        
        transform.position = position;
        transform.rotation = Quaternion.identity;
        
        _currentPenetrationCount = 0;
        _timeAlive = 0f;
    }

    public virtual void Fire(Vector3 targetPosition)
    {
        _targetPosition = targetPosition;
        RotateToTarget(targetPosition);

        isAvaliable = false;
        mesh.gameObject.SetActive(true);
        gameObject.SetActive(true);

        impactEffect.Stop();

        _isMove = true;
    }

    protected virtual void RotateToTarget(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        direction.y = 0;
        direction.Normalize();

        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
    }

    private void FixedUpdate()
    {
        if (_isMove)
        {
            Move();
            Living();
        }
    }

    protected abstract void Move();
    protected virtual void Living()
    {
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

    private void OnTriggerEnter(Collider other)
    {
        Hit(other);
    }

    protected virtual void Hit(Collider collider)
    {
        if(!isCanAttack) return;

        if (collider.gameObject.TryGetComponent<IDamagable>(out IDamagable damagable))
        {
            ApplyDamage(damagable.GetHealth());
            isCanAttack = false;
        }

        if (_currentPenetrationCount >= maxPenetrationCount)
        {
            FinishProjectile();
        }
        else
        {
            Penetration();
            isCanAttack = true;
        }
    }
    
    private IEnumerator ImpactEffect()
    {
        impactEffect.Play();
        yield return new WaitWhile(() => impactEffect.IsAlive(true));
        isAvaliable = true;

        gameObject.SetActive(false);
    }

    protected virtual void FinishProjectile()
    {
        _targetPosition = Vector3.zero;
        mesh.gameObject.SetActive(false);

        StartCoroutine(nameof(ImpactEffect));
    }

    protected virtual void Penetration()
    {
        _currentPenetrationCount++;
    }
}