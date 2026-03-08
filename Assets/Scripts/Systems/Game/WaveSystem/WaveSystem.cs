using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class WaveSystem : MonoBehaviour
{
    public System.Action<SCompleteWaveInfo> sendResults;
    public System.Action finishWaves;
    public System.Action OnCompleteWave;
    
    public System.Action<int> updateNumberWave;
    public System.Action<int> updateCountEnemies;

    [SerializeField, Range(1f, 50f)] private float minSpawnRadius;
    [SerializeField, Range(1f, 50f)] private float maxSpawnRadius;
    [SerializeField, Range(0.1f, 10f)] private float spawnSpeed;
    [SerializeField] List<Wave> waves;

    private int currentWave;
    private Coroutine spawningEnemies;
    private int countWaveEnemies;

    private SCompleteWaveInfo completeWaveInfo;
    private int _defeatEnemies;

    private PlayerCharacter playerTarget;

    private bool _isInfinityWaves;
    public bool IsInfinityWaves => _isInfinityWaves;

    public void Initialize(PlayerCharacter playerTarget)
    {
        this.playerTarget = playerTarget;

        currentWave = 0;
    }

    public void StartWave()
    {
        updateNumberWave?.Invoke(currentWave + 1);
        spawningEnemies = StartCoroutine(nameof(SpawningEnemies));
    }

    public void StartInfinityWaves()
    {
        _isInfinityWaves = true;

        CreateNewWave();
        StartWave();
    }

    private IEnumerator SpawningEnemies()
    {
        countWaveEnemies = waves[currentWave].Count;
        updateCountEnemies?.Invoke(countWaveEnemies);

        for (int i = 0; i < waves[currentWave].Count; i++)
        {
            yield return new WaitForSeconds(spawnSpeed);

            Vector3 _spawnPosition = GetSpawnPosition();
            
            Enemy _enemy = Instantiate(
                waves[currentWave].GetRandomEnemy(), 
                _spawnPosition, 
                GetSpawnRotation(_spawnPosition)
            );

            _enemy.Initialize();
            _enemy.SetPlayerTarget(playerTarget);

            _enemy.GetHealth().OnDead += DeathEnemy;
        }

        spawningEnemies = null;
    }

    public void StopWave()
    {
        if(spawningEnemies != null)
        {
            StopCoroutine(spawningEnemies);
        }
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
        completeWaveInfo = new SCompleteWaveInfo(
            0,
            _defeatEnemies,
            currentWave
        );

        sendResults?.Invoke(completeWaveInfo);
    }

    private Vector3 GetSpawnPosition()
    {
        for (int attempt = 0; attempt < 100; attempt++)
        {
            Vector3 _position = GetRandomPointInDonut(playerTarget.transform.position);

            if (NavMesh.SamplePosition(_position, out NavMeshHit hit, 2f, NavMesh.AllAreas))
                return hit.position;
        }

        return Vector3.zero;
    }

    private Vector3 GetRandomPointInDonut(Vector3 _center)
    {
        float angle = Random.Range(0f, Mathf.PI * 2f);
        float radius = Random.Range(minSpawnRadius, maxSpawnRadius);

        float x = _center.x + Mathf.Cos(angle) * radius;
        float z = _center.z + Mathf.Sin(angle) * radius;

        return new Vector3(x, 1f, z);
    }

    private Quaternion GetSpawnRotation(Vector3 _spawnPosition)
    {
        Vector3 direction = playerTarget.transform.position - _spawnPosition;
        return Quaternion.LookRotation(direction, Vector3.up);
    }

    private void DeathEnemy()
    {
        _defeatEnemies++;
        
        countWaveEnemies--;
        updateCountEnemies?.Invoke(countWaveEnemies);

        if (countWaveEnemies <= 0)
        {
            CompleteWave();
        }
    }
    
    private void CreateNewWave()
    {
        List<Enemy> _enemies = waves.Last().Enemies;
        int _count = Mathf.RoundToInt(waves.Last().Count * 1.1f);

        Wave _wave = new Wave(_enemies, _count);
        waves.Add(_wave);
    }
}