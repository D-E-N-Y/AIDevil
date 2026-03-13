using System.Collections.Generic;
using UnityEngine;

public class WaveGenerator
{
    public Wave CreateNextWave(Wave previousWave)
    {
        List<Enemy> enemies = new List<Enemy>(previousWave.Enemies);

        int count = Mathf.RoundToInt(previousWave.Count * 1.1f);

        return new Wave(enemies, count);
    }
}