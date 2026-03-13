using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Waves", menuName = "Waves", order = 0)]
public class WaveConfig : ScriptableObject 
{
    [SerializeField] private List<Wave> _waves;
    public IReadOnlyList<Wave> Waves => _waves;

    public int GetEnemyCount()
    {
        int count = 0;

        _waves.ForEach(wave => count += wave.Count);

        return count;
    }

    public int GetWavesCount()
    {
        return _waves.Count;
    }
}