using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(EnemyManager))]
public class WaveSystem : MonoBehaviour
{
    public event System.Action OnFinishWaves;
    public event System.Action OnCompleteWave;
    
    public System.Action<int> updateNumberWave;

    private List<Wave> _waves;

    private int currentWave;

    private bool _isInfinityWaves;
    public bool IsInfinityWaves => _isInfinityWaves;

    private EnemyManager _enemyManager;
    public EnemyManager EnemyManager => _enemyManager;

    private WaveGenerator _waveGenerator;

    public void Initialize(IReadOnlyList<Wave> waves, WorldPickupSystem worldPickupSystem, PlayerCharacter playerTarget)
    {
        _waves = waves.ToList();
        
        _enemyManager = GetComponent<EnemyManager>();
        _enemyManager.Initialize(playerTarget.transform, worldPickupSystem);
        _enemyManager.onAllEnemiesDead += CompleteWave;

        _waveGenerator = new WaveGenerator();

        currentWave = 0;
    }

    public void StartWave()
    {
        updateNumberWave?.Invoke(currentWave + 1);
        _enemyManager.StartSpawn(_waves[currentWave]);
    }

    public void StartInfinityWaves()
    {
        _isInfinityWaves = true;

        CreateNewWave();
        StartWave();
    }

    public void StopWave()
    {
        _enemyManager.StopSpawn();
    }

    public void CompleteWave()
    {
        Debug.Log("Complete Wave");
        
        currentWave++;

        if(_isInfinityWaves)
        {
            CreateNewWave();
        }

        if(currentWave >= _waves.Count) 
        {
            OnFinishWaves?.Invoke();
        }
        else
        {
            OnCompleteWave?.Invoke();
        }
    }

    public SCompleteWaveInfo GetWaveResult()
    {
        SCompleteWaveInfo _waveResult = new SCompleteWaveInfo(
            _enemyManager.DefeatEnemies,
            currentWave
        );

        return _waveResult;
    }

    private void CreateNewWave()
    {
        Wave lastWave = _waves.Last();
        Wave newWave = _waveGenerator.CreateNextWave(lastWave);

        _waves.Add(newWave);
    }
}