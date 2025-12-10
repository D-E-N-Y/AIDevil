using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaveSystem : MonoBehaviour
{
    public System.Action<SCompleteWaveInfo> sendResults;
    public System.Action<ESessionResult> finishWaves;
    
    [SerializeField, Range(1f, 50f)] private float minSpawnRadius;
    [SerializeField, Range(1f, 50f)] private float maxSpawnRadius;
    [SerializeField, Range(0.1f, 10f)] private float spawnSpeed;
    [SerializeField, Range(0.1f, 10f)] private float timeBetweenWaves;
    [SerializeField] List<WaveData> waves;
    private int currentWave;
    private Coroutine spawningEnemies;
    private int countWaveEnemies;

    private SCompleteWaveInfo completeWaveInfo;
    private int _defeatEnemies;

    private Player playerTarget;

    public void Initialize(Player playerTarget)
    {
        this.playerTarget = playerTarget;

        currentWave = 0;
    }

    public void StartWave()
    {
        spawningEnemies = StartCoroutine(nameof(SpawningEnemies));
    }

    private IEnumerator SpawningEnemies()
    {
        int countEnemies = 0;
        waves[currentWave].enemies.ForEach(x => countEnemies += x.count);
        countWaveEnemies = countEnemies;

        foreach (WaveEnemyData enemies in waves[currentWave].enemies)
        {
            for (int i = 0; i < enemies.count; i++)
            {
                yield return new WaitForSeconds(spawnSpeed);

                Vector3 _spawnPosition = GetSpawnPosition();
                Enemy _enemy = Instantiate(enemies.enemy, _spawnPosition, GetSpawnRotation(_spawnPosition));

                _enemy.Initialize();
                _enemy.SetPlayerTarget(playerTarget);

                _enemy.onDead += DeathEnemy;
            }
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

        if(currentWave >= waves.Count) 
        {
            finishWaves?.Invoke(ESessionResult.WIN);
        }
        else
        {
            StartWave();
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

    private void DeathEnemy(IHealth _enemy)
    {
        _defeatEnemies++;
        countWaveEnemies--;

        if (countWaveEnemies <= 0)
        {
            CompleteWave();
        }
    }
}