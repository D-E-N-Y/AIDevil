using UnityEngine;

public class WorldPickupSystem : MonoBehaviour 
{
    [SerializeField] private Transform _pickupContainer;
    
    private List<WorldPickup> _worldPickups;
}