using System;
using System.Collections;
using UnityEngine;

public abstract class Projectile : Weapon
{
    [SerializeField] protected Transform mesh;
    [SerializeField] private ParticleSystem impactEffect;

    [SerializeField, Range(1f, 100f)] protected float moveSpeed;

    protected Vector3 _targetPosition;

    public bool isCanAttack { get; protected set; }
    public bool isAvaliable { get; protected set; }

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
    }

    public virtual void Fire(Vector3 targetPosition)
    {
        _targetPosition = targetPosition;
        RotateToTarget(targetPosition);

        isAvaliable = false;
        mesh.gameObject.SetActive(true);
        gameObject.SetActive(true);
    }

    protected void RotateToTarget(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        direction.y = 0;
        direction.Normalize();

        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
    }

    private void FixedUpdate()
    {
        Move();
    }

    protected abstract void Move();

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<IUnit>(out IUnit unit))
        {
            if(!isCanAttack) return;
            
            ApplyDamage(unit.GetHealth());

            isCanAttack = false;
        }
        else if (other.gameObject.TryGetComponent<IHealth>(out IHealth actor))
        {
            if(!isCanAttack) return;
            
            ApplyDamage(actor);

            isCanAttack = false;
        }

        _targetPosition = Vector3.zero;
        mesh.gameObject.SetActive(false);

        StartCoroutine(nameof(ImpactEffect));
    }
    
    private IEnumerator ImpactEffect()
    {
        impactEffect.Play();
        yield return new WaitWhile(() => impactEffect.IsAlive(true));
        isAvaliable = true;

        gameObject.SetActive(false);
    }
}