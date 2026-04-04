using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class PickupSensor : MonoBehaviour 
{
    private float _radius = 1f;
    public float Radius => _radius;

    private float _pickupRangeModifier;
    public float PickUpRangeModifier => _pickupRangeModifier;
    
    private SphereCollider _sphereCollider;

    private UnitContext _context;

    public void Initialize(UnitContext context)
    {
        if(_context != null) 
        {
            ClearSubscriptions();
        }

        _context = context;

        _sphereCollider = GetComponent<SphereCollider>();
        _sphereCollider.isTrigger = true;

        gameObject.layer = LayerMask.NameToLayer("PickupSensor");

        AddSubscriptions();
        SetStats();
    }

    protected void UpdateStats(StatType statType)
    {
        if(statType == StatType.PickUpRangeModifier)
        {
            SetStats();
        }
    }

    protected virtual void SetStats()
    {
        _pickupRangeModifier = ((PlayerCharacterStats)_context.Stats).PickUpRangeModifier;
        _sphereCollider.radius = _radius * _pickupRangeModifier;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (!other.TryGetComponent(out WorldPickup worldPickup)) return;

        worldPickup.PickUp(_context);
    }

    protected virtual void ClearSubscriptions()
    {
        _context.Stats.OnStatChanged -= UpdateStats;
    }

    protected virtual void AddSubscriptions() 
    {
        _context.Stats.OnStatChanged += UpdateStats;
    }


    private void OnDestroy()
    {
        ClearSubscriptions();
    }
}