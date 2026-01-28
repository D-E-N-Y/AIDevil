using System;
using UnityEngine;

public abstract class MeleeWeapon : MonoBehaviour
{
    public Action onSuccessfulAttack;

    [SerializeField, Range(1, 100)] protected int damage;
    protected float _rangeAttack;
    protected string _originLayer;

    public virtual void Initialize(string originLayer, float rangeAttack)
    {
        _originLayer = originLayer + "MeleeWeapon";
        gameObject.layer = LayerMask.NameToLayer(_originLayer);

        _rangeAttack = rangeAttack;
    }

    public virtual void StartAttack()
    {
        gameObject.SetActive(true);
    }

    public virtual void FinishAttack()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<IUnit>(out IUnit unit))
        {
            unit.GetHealth().TakeDamage(damage);
            onSuccessfulAttack?.Invoke();
        }
    }

    public float RangeAttack() => _rangeAttack;
}