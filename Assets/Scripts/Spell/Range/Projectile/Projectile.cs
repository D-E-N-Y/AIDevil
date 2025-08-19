using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Action onSuccessfulAttack;

    [SerializeField, Range(1, 100)] protected int damage;
    [SerializeField, Range(1f, 100f)] protected float moveSpeed;
    protected string _originLayer;
    protected Vector3 _targetPosition;

    public bool isAvaliable { get; protected set; }

    public virtual void Initialize(string originLayer, Vector3 _position)
    {
        _originLayer = originLayer + "Projectile";
        gameObject.layer = LayerMask.NameToLayer(_originLayer);

        transform.position = _position;
    }

    public virtual void Fire(Vector3 targetPosition)
    {
        _targetPosition = targetPosition;
        transform.LookAt(_targetPosition);

        isAvaliable = false;
        gameObject.SetActive(true);
    }

    void Update()
    {
        if (_targetPosition == null) return;

        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out MonoBehaviour comp) &&
            comp is IHealth _unit)
        {
            _unit.TakeDamage(damage);
            onSuccessfulAttack?.Invoke();
        }

        isAvaliable = true;
        gameObject.SetActive(false);
    }
}