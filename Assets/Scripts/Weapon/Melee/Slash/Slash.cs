using System;
using UnityEngine;

public abstract class Slash : MeleeWeapon 
{
    [SerializeField, Range(0f, 10f)] protected float distanceFromOrigin;
    [SerializeField, Range(0f, 360f)] protected float slashAngle;

    [SerializeField] protected Transform tipWeapon; 

    protected float _angleStepPerFrame;
    protected Vector3 _attackDir;
    protected Transform _origin;

    public override void PrepareAttack(Transform origin, Vector3 target)
    {
        _origin = origin;
        
        _angleStepPerFrame = slashAngle / _timeToLive * Time.fixedDeltaTime;

        _attackDir = (target - _origin.position).normalized;
        _attackDir = Quaternion.AngleAxis(slashAngle / 2, _origin.up) * _attackDir;
    }

    public override void FinishAttack()
    {
        _ignoreTargets.Clear();
        
        base.FinishAttack();
    }

    void FixedUpdate()
    {
        Move();
        RaycastDamage();
    }

    protected void Move()
    {
        Vector3 newPosition = _origin.position + _attackDir * distanceFromOrigin;
        
        transform.position = newPosition;
        transform.forward = _attackDir;

        _attackDir = Quaternion.AngleAxis(-_angleStepPerFrame, _origin.up) * _attackDir;
    }

    protected virtual void RaycastDamage()
    {
        Vector3 startPos = _origin.position;
        Vector3 endPos = tipWeapon.position; 
        
        Collider[] hits = Physics.OverlapCapsule(startPos, endPos, 0.5f, _interactLayers);

        foreach (Collider hit in hits)
        {
            if (_ignoreTargets.Contains(hit))
                continue;

            if (hit.gameObject.TryGetComponent<IDamagable>(out IDamagable damagable))
            {
                ApplyDamage(damagable.IHealth);
                _ignoreTargets.Add(hit);
            }
        }
    }
}