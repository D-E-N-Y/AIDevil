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

    public bool isAvaliable { get; protected set; }

    public virtual void Initialize(UnitFaction unitFaction, Vector3 position)
    {
        _originLayer = unitFaction + "Projectile";
        gameObject.layer = LayerMask.NameToLayer(_originLayer);

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
        if (other.gameObject.TryGetComponent<IHealth>(out IHealth unit))
        {
            unit.TakeDamage(damage);
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
}