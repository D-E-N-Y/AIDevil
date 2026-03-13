using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Wave
{
    [SerializeField] private List<Enemy> _enemies;
    public List<Enemy> Enemies => _enemies;

    [SerializeField, Range(1, 999)] private int _count;
    public int Count => _count;

    public Wave(List<Enemy> enemies, int count = 1)
    {
        _enemies = enemies;
        _count = count;
    }

    public Enemy GetRandomEnemy()
    {
        return _enemies[UnityEngine.Random.Range(0, _enemies.Count)];
    }
}