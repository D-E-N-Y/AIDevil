using System;
using System.Collections.Generic;

[Serializable]
public struct WaveData
{
    public List<WaveEnemyData> enemies;

    public WaveData(List<WaveEnemyData> enemies)
    {
        this.enemies = enemies;
    }
}