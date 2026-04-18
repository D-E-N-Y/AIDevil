using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(EnemyManager))]
public class WaveSystem : MonoBehaviour
{
    public System.Action<SCompleteWaveInfo> sendResults;
    public System.Action finishWaves;
    public System.Action OnCompleteWave;
    
    public System.Action<int> updateNumberWave;

    private List<Wave> _waves;

    private int currentWave;

    private bool _isInfinityWaves;
    public bool IsInfinityWaves => _isInfinityWaves;

    private EnemyManager _enemyManager;
    public EnemyManager EnemyManager => _enemyManager;

    private WaveGenerator _waveGenerator;

    public void Initialize(IReadOnlyList<Wave> waves, PlayerCharacter playerTarget)
    {
        _waves = waves.ToList();
        
        _enemyManager = GetComponent<EnemyManager>();
        _enemyManager.Initialize(playerTarget.transform);
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

        SendWaveResults();

        if(_isInfinityWaves)
        {
            CreateNewWave();
        }

        if(currentWave >= _waves.Count) 
        {
            finishWaves?.Invoke();
        }
        else
        {
            OnCompleteWave?.Invoke();
        }
    }

    public void SendWaveResults()
    {
        SCompleteWaveInfo _completeWaveInfo = new SCompleteWaveInfo(
            0,
            _enemyManager.DefeatEnemies,
            currentWave
        );

        sendResults?.Invoke(_completeWaveInfo);
    }

    private void CreateNewWave()
    {
        Wave lastWave = _waves.Last();
        Wave newWave = _waveGenerator.CreateNextWave(lastWave);

        _waves.Add(newWave);
    }
}