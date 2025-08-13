using System;

[Serializable]
public struct WaveEnemyData
{
    public Enemy enemy;
    public int count;

    public WaveEnemyData(Enemy enemy, int count)
    {
        this.enemy = enemy;
        this.count = count;
    }
}