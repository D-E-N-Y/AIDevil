using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WP_RicochetBeam : Projectile 
{
    [SerializeField, Range(1f, 10f)] private float ricochetRadius;
    private IDamagable currentDamagable;    

    protected override void Move()
    {
        transform.position += transform.forward * moveSpeed * Time.fixedDeltaTime;
    }

    protected override void Hit(Collider collider)
    {
        if(!isCanAttack) return;
        if (_ignoreTargets.Contains(collider)) return;
        
        if (collider.gameObject.TryGetComponent<IDamagable>(out IDamagable damagable))
        {
            currentDamagable = damagable;
            
            ApplyDamage(damagable.IHealth);
            isCanAttack = false;
        }

        if (_currentPenetrationCount >= maxPenetrationCount)
        {
            FinishAttack();
        }
        else
        {
            Penetration();
            isCanAttack = true;
        }
    }

    protected override void Penetration()
    {
        base.Penetration();

        Transform nearestTarget = GetNearestTarget();

        if (nearestTarget != null)
        {
            RotateToTarget(nearestTarget.position);
        }
    }

    public Transform GetNearestTarget()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, ricochetRadius, _interactLayers);
        List<IDamagable> damagables = new List<IDamagable>();

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out IDamagable damagable))
            {
                if (currentDamagable == damagable) continue;
                
                damagables.Add(damagable);
            }
        }

        if (damagables.Count > 0)
        {
            return damagables
                .OfType<MonoBehaviour>()
                .OrderBy(u => Vector3.Distance(transform.position, u.transform.position))
                .FirstOrDefault()
                ?.transform;
        }
        else
        {
            return null;
        }
    }
}