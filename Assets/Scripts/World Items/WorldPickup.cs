using UnityEngine;

public abstract class WorldPickup : MonoBehaviour 
{
    protected bool _canPickUp = true;
    public bool CanPickUp => _canPickUp;

    public abstract void PickUp(UnitContext context);

    public void AllowPickUp() => _canPickUp = true;
    public void DisallowPickUp() => _canPickUp = false;
}