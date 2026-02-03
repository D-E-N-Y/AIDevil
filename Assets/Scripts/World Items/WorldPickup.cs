using System.Runtime.InteropServices;
using UnityEngine;

public abstract class WorldPickup : MonoBehaviour 
{
    public abstract void PickUp(ItemContext context);
}