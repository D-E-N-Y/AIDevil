using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Sensor : MonoBehaviour
{
    private string _originLayer;

    public event Action<IDamagable> OnUnitEnter;
    public event Action<IDamagable> OnUnitExit;

    private List<IDamagable> _damagables;
    public IReadOnlyList<IDamagable> Damagables => _damagables;

    private SphereCollider _sphereCollider;

    public void Initialize(UnitFaction unitFaction, float radius)
    {
        _originLayer = unitFaction + "Sensor";
        gameObject.layer = LayerMask.NameToLayer(_originLayer);

        _damagables = new List<IDamagable>();

        _sphereCollider = GetComponent<SphereCollider>();
        _sphereCollider.radius = radius;
        _sphereCollider.isTrigger = true;
    }

    public void SearchInCollision()
    {
        int mask = 1 << LayerMask.NameToLayer(_originLayer);
        Collider[] hits = Physics.OverlapSphere(transform.position, _sphereCollider.radius, mask);

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out IDamagable damagable) && !_damagables.Contains(damagable))
            {
                _damagables.Add(damagable);
                damagable.OnDead += OnUnitDead;

                OnUnitEnter?.Invoke(damagable);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out IDamagable damagable)) return;
        if (_damagables.Contains(damagable)) return;

        _damagables.Add(damagable);
        damagable.OnDead += OnUnitDead;

        OnUnitEnter?.Invoke(damagable);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent(out IDamagable unit)) return;
            
        RemoveUnit(unit);
    }

    void OnUnitDead(IDamagable unit)
    {
        RemoveUnit(unit);
    }

    private void RemoveUnit(IDamagable damagable)
    {
        if (!_damagables.Remove(damagable)) return;

        damagable.OnDead -= OnUnitDead;
        OnUnitExit?.Invoke(damagable);
    }

    public void Clear()
    {
        if (IsHasUnits())
        {
            for (int i = _damagables.Count - 1; i >= 0; i--)
            {
                RemoveUnit(_damagables[i]);
            }
        }

        _damagables.Clear();
    }

    public Transform GetNearestTarget()
    {
        return _damagables
            .OfType<MonoBehaviour>()
            .OrderBy(u => Vector3.Distance(transform.position, u.transform.position))
            .FirstOrDefault()
            ?.transform;
    }

    public bool IsHasUnits() => _damagables.Count > 0;
}