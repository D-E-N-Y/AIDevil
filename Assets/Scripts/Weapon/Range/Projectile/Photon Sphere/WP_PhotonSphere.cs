using UnityEngine;

public class WP_PhotonSphere : Projectile
{
    [Header("Explose")]
    [SerializeField, Range(0.1f, 5f)] private float exploseRadius = 2f;

    protected override void Move()
    {
        transform.position += transform.forward * moveSpeed * Time.fixedDeltaTime;
    }

    protected override void Hit(Collider collider)
    {
        if(!isCanAttack) return;
        
        if (_currentPenetrationCount >= maxPenetrationCount)
        {
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