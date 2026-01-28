using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class ItemSensor : MonoBehaviour 
{
    [SerializeField] private float _radius = 1f;
    public float Radius => _radius;
    
    private SphereCollider _sphereCollider;

    private Inventory _inventory;

    public void Initialize(Inventory inventory)
    {
        _inventory = inventory;

        _sphereCollider = GetComponent<SphereCollider>();
        _sphereCollider.radius = _radius;
        _sphereCollider.isTrigger = true;

        gameObject.layer = LayerMask.NameToLayer("ItemSensor");
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (!other.TryGetComponent(out WorldItem worldItem)) return;

        worldItem.PickUp(_inventory);
    }
}