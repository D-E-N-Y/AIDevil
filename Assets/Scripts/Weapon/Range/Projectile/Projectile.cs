using System;
using System.Collections;
using UnityEngine;

public abstract class Projectile : RangeWeapon
{
    [SerializeField, Range(1f, 100f)] protected float moveSpeed;

    protected bool _isMove;
    public bool IsMove => _isMove; 

    public override void PrepareAttack(Transform fireTransfrom, Vector3 target)
    {
        isCanAttack = true;
        
        Vector3 firePos = fireTransfrom.position;

        transform.position = firePos;
        transform.rotation = Quaternion.identity;
        
        _currentPenetrationCount = 0;
        _timeAlive = 0f;

        _ignoreTargets.Clear();

        RotateToTarget(target);
    }

    public override void StartAttack()
    {
        base.StartAttack();
        
        isAvaliable = false;
        
        mesh.gameObject.SetActive(true);
        gameObject.SetActive(true);

        impactEffect.Stop();

        _isMove = true;
    }

    public override void FinishAttack()
    {
        _isMove = false;
        isAlive = false;

        mesh.gameObject.SetActive(false);
        StartCoroutine(nameof(ImpactEffect));
    }

    protected void FixedUpdate()
    {
        if (_isMove)
        {
            Move();
        }

        Living();
    }

    protected abstract void Move();

    private void OnTriggerEnter(Collider other)
    {
        Hit(other);
    }

    protected virtual void Hit(Collider collider)
    {
        if(!isCanAttack) return;
        if (_ignoreTargets.Contains(collider)) return;

        if (collider.gameObject.TryGetComponent<IDamagable>(out IDamagable damagable))
        {
            ApplyDamage(damagable.GetHealth());

            if (_currentPenetrationCount >= maxPenetrationCount)
            {
                isCanAttack = false;
                FinishAttack();
            }
            else
            {
                Penetration();
                isCanAttack = true;
            }
        }
    }
    
    private IEnumerator ImpactEffect()
    {
        impactEffect.Play();
        yield return new WaitWhile(() => impactEffect.IsAlive(true));
        isAvaliable = true;

        gameObject.SetActive(false);
    }
}