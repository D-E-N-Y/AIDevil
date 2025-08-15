using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class BotSensor : MonoBehaviour
{
    [SerializeField] protected LayerMask targetLayer;

    // public Action onEnterTrigger;
    // public Action onExitTrigger;

    private List<IHealth> _units;

    public void Initialize()
    {
        _units = new List<IHealth>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsCorrentTarget(other.gameObject) &&
            other.gameObject.TryGetComponent(out MonoBehaviour comp) &&
            comp is IHealth unit &&
            !_units.Contains(unit))
        {
            _units.Add(unit);
            unit.onDead += RemoveTargetUnit;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsCorrentTarget(other.gameObject) &&
            other.gameObject.TryGetComponent(out MonoBehaviour comp) &&
            comp is IHealth unit &&
            _units.Contains(unit))
        {
            RemoveTargetUnit(unit);
        }
    }

    private void RemoveTargetUnit(IHealth unit)
    {
        _units.Remove(unit);
        unit.onDead -= RemoveTargetUnit;
    }

    protected bool IsCorrentTarget(GameObject gameObject)
    {
        return targetLayer == (targetLayer | (1 << gameObject.layer));
    }

    public bool IsHasUnits()
    {
        return _units.Count > 0;
    }
}