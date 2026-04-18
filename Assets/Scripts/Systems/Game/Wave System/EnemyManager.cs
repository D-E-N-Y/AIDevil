using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public System.Action<int> updateCountEnemies;
    public System.Action onAllEnemiesDead;
    
    private Coroutine spawningEnemies;
    private Wave _wave;

    private int countWaveEnemies;

    [SerializeField, Range(1f, 50f)] private float minSpawnRadius;
    [SerializeField, Range(1f, 50f)] private float maxSpawnRadius;
    [SerializeField, Range(0.1f, 10f)] private float spawnSpeed;

    [SerializeField] private Transform _enemyContainer;

    private int _defeatEnemies;
    public int DefeatEnemies => _defeatEnemies;

    private Transform _target;
    private WorldPickupSystem _worldPickupSystem;

    private Dictionary<EnemyType, List<Enemy>> _enemies;

    public void Initialize(Transform target, WorldPickupSystem worldPickupSystem)
    {
        _target = target;
        _worldPickupSystem = worldPickupSystem;

        _enemies = new Dictionary<EnemyType, List<Enemy>>();
        foreach (EnemyType type in System.Enum.GetValues(typeof(EnemyType)))
        {
            _enemies[type] = new List<Enemy>();
        }
    }

    public void StartSpawn(Wave wave)
    {
        _wave = wave;
        
        spawningEnemies = StartCoroutine(SpawningEnemies());
    }

    public void StopSpawn()
    {
        if(spawningEnemies != null)
        {
            StopCoroutine(spawningEnemies);
        }

        spawningEnemies = null;
    }

    private IEnumerator SpawningEnemies()
    {
        countWaveEnemies = _wave.Count;
        updateCountEnemies?.Invoke(countWaveEnemies);

        for (int i = 0; i < _wave.Count; i++)
        {
            yield return new WaitForSeconds(spawnSpeed);
            
            Enemy _randomEnemy = _wave.GetRandomEnemy();
            Enemy _enemy = GetEnemy(_randomEnemy);

            if (_enemy == null)
            {
                CreateEnemy(_randomEnemy);
            }
            else
            {
                RespawnEnemy(_enemy);
            }
        }

        spawningEnemies = null;
    }

    private void CreateEnemy(Enemy enemy)
    {
        Vector3 _spawnPosition = GetSpawnPosition();
        
        Enemy _enemy = Instantiate(
            enemy, 
            _spawnPosition, 
            GetSpawnRotation(_spawnPosition)
        );
        _enemy.transform.SetParent(_enemyContainer);
        
        _enemy.Initialize();

        _enemy.Health.OnDead -= DeathEnemy;
        _enemy.Health.OnDead += DeathEnemy;

        _enemy.OnDead -= HandleEnemyDead;
        _enemy.OnDead += HandleEnemyDead;

        _enemy.SetTarget(_target);

        _enemies[_enemy.Type].Add(_enemy);
    }

    private void RespawnEnemy(Enemy enemy)
    {
        Vector3 _spawnPosition = GetSpawnPosition();
        
        enemy.transform.SetPositionAndRotation(
            _spawnPosition,
            GetSpawnRotation(_spawnPosition)
        );

        enemy.Initialize();
        
        enemy.Health.OnDead -= DeathEnemy;
        enemy.Health.OnDead += DeathEnemy;

        enemy.OnDead -= HandleEnemyDead;
        enemy.OnDead += HandleEnemyDead;

        enemy.SetTarget(_target);
    }

    private void HandleEnemyDead(IDamagable damagable)
    {
        Enemy enemy = (Enemy)damagable;
        _worldPickupSystem.SpawnResource(ResourceType.Credits, enemy.transform.position, enemy.DropMoney);

        GameInstance.current.ProfileManager.CurrentProfile.BestiaryProgress.AddEnemy(enemy.Name);
    }

    private Enemy GetEnemy(Enemy enemy)
    {
        foreach (Enemy _enemy in _enemies[enemy.Type])
        {
            if (enemy.ID == _enemy.ID && _enemy.IsDead)
            {
                return _enemy;
            }
        }

        return null;
    }

    private Vector3 GetSpawnPosition()
    {
        for (int attempt = 0; attempt < 100; attempt++)
        {
            Vector3 _position = GetRandomPointInDonut(_target.transform.position);

            if (UnityEngine.AI.NavMesh.SamplePosition(_position, out UnityEngine.AI.NavMeshHit hit, 2f, UnityEngine.AI.NavMesh.AllAreas))
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
        Vector3 direction = _target.transform.position - _spawnPosition;
        return Quaternion.LookRotation(direction, Vector3.up);
    }

    private void DeathEnemy()
    {
        _defeatEnemies++;
        
        countWaveEnemies--;
        updateCountEnemies?.Invoke(countWaveEnemies);

        if (countWaveEnemies <= 0)
        {
            onAllEnemiesDead?.Invoke();
        }
    }
}