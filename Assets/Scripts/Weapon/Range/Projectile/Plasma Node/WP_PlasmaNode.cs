using System.Collections;
using System.Linq;
using UnityEngine;

public class WP_PlasmaNode : Projectile
{
    [SerializeField, Range(0.1f, 10f)] private float timeAttacking = 2f;
    [SerializeField, Range(0.01f, 1f)] private float intevalAttack = 0.25f;

    [SerializeField, Range(0.1f, 5f)] private float radiusAttack = 2f;
    [SerializeField] private Sensor sensor;

    [SerializeField] private ParticleSystem attackingEffect;

    public override void Initialize(UnitFaction unitFaction)
    {
        base.Initialize(unitFaction);

        sensor.Initialize(unitFaction, radiusAttack);
        sensor.gameObject.SetActive(false);

        attackingEffect.Stop();
        attackingEffect.gameObject.SetActive(false);
    }

    protected override void Move()
    {
        transform.position += transform.forward * moveSpeed * Time.fixedDeltaTime;
    }

    protected override void Hit(Collider collider)
    {
        if(!isCanAttack) return;
        if (_ignoreTargets.Contains(collider)) return;
        
        if (_currentPenetrationCount >= maxPenetrationCount)
        {
            isCanAttack = false;
            _isMove = false;

            sensor.gameObject.SetActive(true);
            sensor.SearchInCollision();

            StartCoroutine(Attacking());
        }
        else
        {
            Penetration();
            isCanAttack = true;
        }        
    }

    private IEnumerator Attacking()
    {
        attackingEffect.gameObject.SetActive(true);
        attackingEffect.Play();
        
        float _timer = 0f;

        while(_timer <= timeAttacking)
        {
            if (sensor.IsHasUnits())
            {
                var targets = sensor.Damagables.ToList();

                foreach (IDamagable damagable in targets)
                {
                    ApplyDamage(damagable.GetHealth());
                }
            }
            
            yield return new WaitForSeconds(intevalAttack);
            _timer += intevalAttack;
        }

        FinishAttack();
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
        
        sensor.Clear();
        sensor.gameObject.SetActive(false);

        attackingEffect.Stop();
        attackingEffect.gameObject.SetActive(false);
    }
}