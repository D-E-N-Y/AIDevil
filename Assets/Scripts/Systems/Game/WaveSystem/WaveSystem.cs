using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(EnemyController))]
public class WaveSystem : MonoBehaviour
{
    public System.Action<SCompleteWaveInfo> sendResults;
    public System.Action finishWaves;
    public System.Action OnCompleteWave;
    
    public System.Action<int> updateNumberWave;

    [SerializeField] List<Wave> waves;

    private int currentWave;

    private bool _isInfinityWaves;
    public bool IsInfinityWaves => _isInfinityWaves;

    private EnemyController _enemyController;
    public EnemyController EnemyController => _enemyController;

    private WaveGenerator _waveGenerator;

    public void Initialize(PlayerCharacter playerTarget)
    {
        _enemyController = GetComponent<EnemyController>();
        _enemyController.Initialize(playerTarget);
        _enemyController.onAllEnemiesDead += CompleteWave;

        _waveGenerator = new WaveGenerator();

        currentWave = 0;
    }

    public void StartWave()
    {
        updateNumberWave?.Invoke(currentWave + 1);
        _enemyController.StartSpawn(waves[currentWave]);
    }

    public void StartInfinityWaves()
    {
        _isInfinityWaves = true;

        CreateNewWave();
        StartWave();
    }

    public void StopWave()
    {
        _enemyController.StopSpawn();
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

        if(currentWave >= waves.Count) 
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
            _enemyController.DefeatEnemies,
            currentWave
        );

        sendResults?.Invoke(_completeWaveInfo);
    }

    private void CreateNewWave()
    {
        Wave lastWave = waves.Last();
        Wave newWave = _waveGenerator.CreateNextWave(lastWave);

        waves.Add(newWave);
    }
}