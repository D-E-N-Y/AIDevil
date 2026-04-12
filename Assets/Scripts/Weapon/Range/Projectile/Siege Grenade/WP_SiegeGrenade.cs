using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WP_SiegeGrenade : Projectile 
{
    [Header("Explose")]
    [SerializeField, Range(0.1f, 5f)] private float exploseRadius = 2f;
    
    private Rigidbody _rb;

    public override void Initialize(UnitFaction unitFaction)
    {
        base.Initialize(unitFaction);

        _rb = GetComponent<Rigidbody>();
    }

    public override void PrepareAttack(Transform fireTransfrom, Vector3 target)
    {
        base.PrepareAttack(fireTransfrom, target);

        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
    }

    protected override void Move()
    {
        _rb.AddForce(transform.forward * moveSpeed, ForceMode.Impulse);
        _isMove = false;
    }

    protected override void Hit(Collider collider)
    {
        if(!isCanAttack) return;
        if (_ignoreTargets.Contains(collider)) return;
        
        if (_currentPenetrationCount >= maxPenetrationCount)
        {
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
            
            isCanAttack = false;
            _isMove = false;

            Explose();
        }
        else
        {
            Penetration();
            isCanAttack = true;
        }        
    }

    private void Explose()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, exploseRadius, _interactLayers);

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out IDamagable damagable))
            {
                ApplyDamage(damagable.GetHealth());
            }
        }

        FinishAttack();
    }
}