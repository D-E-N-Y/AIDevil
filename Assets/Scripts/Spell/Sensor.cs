using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Sensor : MonoBehaviour
{
    private string _originLayer;

    public Action onEnterTrigger;
    public Action onExitTrigger;

    public Action onEnterUnit;
    public Action onExitUnit;

    private List<IHealth> _units;

    private SphereCollider _sphereCollider;

    public void Initialize(string originLayer, float radius)
    {
        _originLayer = originLayer + "Sensor";
        gameObject.layer = LayerMask.NameToLayer(_originLayer);

        _units = new List<IHealth>();

        _sphereCollider = GetComponent<SphereCollider>();
        _sphereCollider.radius = radius;
        _sphereCollider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<IHealth>(out IHealth unit) &&
            !_units.Contains(unit))
        {
            _units.Add(unit);
            unit.onDead += RemoveTargetUnit;

            onEnterUnit?.Invoke();
        }

        onEnterTrigger?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<IHealth>(out IHealth unit) &&
            _units.Contains(unit))
        {
            RemoveTargetUnit(unit);

            onExitUnit?.Invoke();
        }

        onExitTrigger?.Invoke();
    }

    private void RemoveTargetUnit(IHealth unit)
    {
        _units.Remove(unit);
        unit.onDead -= RemoveTargetUnit;
    }

    public Vector3 GetNerbyUnitPosition()
    {
        Vector3 _nearbyUnitPosition = _units
            .OrderBy(x => Vector3.Distance(transform.position, ((MonoBehaviour)x).transform.position))
            .Select(x => ((MonoBehaviour)x).transform.position)
            .FirstOrDefault();

        return _nearbyUnitPosition;
    }

    public bool IsHasUnits()
    {
        return _units.Count > 0;
    }
}