using System;
using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected Transform mesh;
    [SerializeField] private ParticleSystem impactEffect;
    public Action onSuccessfulAttack;

    [SerializeField, Range(1, 100)] protected int damage;
    [SerializeField, Range(1f, 100f)] protected float moveSpeed;
    protected string _originLayer;
    protected Vector3 _targetPosition;

    protected float _damageModifier;
    protected float _criticalDamageChance;
    protected float _criticalDamageModifier;
    protected float _areaModifier;

    public bool isAvaliable { get; protected set; }

    public virtual void Initialize(UnitFaction unitFaction)
    {
        _originLayer = unitFaction + "Projectile";
        gameObject.layer = LayerMask.NameToLayer(_originLayer);

        isAvaliable = true;
    }

    public virtual void SetToFire(Vector3 position, float damageModifier = 1f, float criticalDamageChance = 0f, float criticalDamageModifier = 1f, float areaModifier = 1f)
    {
        _damageModifier = damageModifier;
        _criticalDamageChance = criticalDamageChance;
        _criticalDamageModifier = criticalDamageModifier;
        _areaModifier = areaModifier;

        transform.localScale = new Vector3(1f * areaModifier, 1f * areaModifier, 1f * areaModifier);

        transform.position = position;
    }

    public virtual void Fire(Vector3 targetPosition)
    {
        _targetPosition = targetPosition;
        RotateToTarget(targetPosition);

        isAvaliable = false;
        mesh.gameObject.SetActive(true);
    }

    protected void RotateToTarget(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        direction.y = 0;
        direction.Normalize();

        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
    }

    private void Update()
    {
        if (_targetPosition == Vector3.zero) return;

        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<IUnit>(out IUnit unit))
        {
            float _damage = damage * _damageModifier;
            
            if(IsCriticalHit())
            {
                _damage *= _criticalDamageModifier;
            }

            unit.GetHealth().TakeDamage(_damage);
            onSuccessfulAttack?.Invoke();
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
    }

    protected bool IsCriticalHit()
    {
        float roll = UnityEngine.Random.Range(0f, 1f);
        return roll < _criticalDamageChance;
    }
}