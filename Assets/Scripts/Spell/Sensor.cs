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

    private List<IDamagable> _units;

    private SphereCollider _sphereCollider;

    public void Initialize(UnitFaction unitFaction, float radius)
    {
        _originLayer = unitFaction + "Sensor";
        gameObject.layer = LayerMask.NameToLayer(_originLayer);

        _units = new List<IDamagable>();

        _sphereCollider = GetComponent<SphereCollider>();
        _sphereCollider.radius = radius;
        _sphereCollider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out IDamagable unit)) return;
        if (_units.Contains(unit)) return;

        _units.Add(unit);
        unit.OnDead += OnUnitDead;

        OnUnitEnter?.Invoke(unit);
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

    private void RemoveUnit(IDamagable unit)
    {
        if (!_units.Remove(unit)) return;

        unit.OnDead -= OnUnitDead;
        OnUnitExit?.Invoke(unit);
    }

    public Transform GetNearestTarget()
    {
        return _units
            .OfType<MonoBehaviour>()
            .OrderBy(u => Vector3.Distance(transform.position, u.transform.position))
            .FirstOrDefault()
            ?.transform;
    }

    public bool IsHasUnits() => _units.Count > 0;
}