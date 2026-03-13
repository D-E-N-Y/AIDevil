using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Level", menuName = "GameLevel", order = 0)]
public class GameLevel : ScriptableObject
{
    [SerializeField] private string _name;
    public string Name => _name;

    [SerializeField] private List<ResourceType> _resources;
    public IReadOnlyList<ResourceType> Resources => _resources;

    [SerializeField] private WaveConfig _waveConfig;
    public WaveConfig WaveConfig => _waveConfig;
}