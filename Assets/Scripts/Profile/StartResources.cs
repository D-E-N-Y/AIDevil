using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Start Resources", menuName = "StaerResources")]
public class StartResources : ScriptableObject 
{
    [SerializeField] private List<Cost> _startResources;
    public IReadOnlyList<Cost> StartResourcesList => _startResources;
}