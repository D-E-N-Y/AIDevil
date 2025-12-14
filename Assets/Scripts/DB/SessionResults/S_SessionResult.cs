using System;

[Serializable]
public struct SSesionResult
{
    public string name;
    public string namePlayerCharacter;
    public ESessionResult result;
    public STime time;
    public int collectCoins;
    public int defeatEnemies;
    public int completedWaves;

    public SSesionResult(string namePlayerCharacter, ESessionResult result, STime time, int collectCoins, int defeatEnemies, int completedWaves)
    {
        name = $"{namePlayerCharacter} {DateTime.Now}";
        
        this.namePlayerCharacter = namePlayerCharacter;
        this.result = result;
        this.time = time;
        this.collectCoins = collectCoins;
        this.defeatEnemies = defeatEnemies;
        this.completedWaves = completedWaves;
    }

    public SSesionResult(string namePlayerCharacter, ESessionResult result, int totalSeconds, int collectCoins, int defeatEnemies, int completedWaves)
    {
        name = $"{namePlayerCharacter} {DateTime.Now}";
        
        this.namePlayerCharacter = namePlayerCharacter;
        this.result = result;
        this.time = new STime(totalSeconds);
        this.collectCoins = collectCoins;
        this.defeatEnemies = defeatEnemies;
        this.completedWaves = completedWaves;
    }
}