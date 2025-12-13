using System;

public struct SSesionResult
{
    public string name;
    public Player playerCharacter;
    public ESessionResult result;
    public STime time;
    public int collectCoins;
    public int defeatEnemies;
    public int completedWaves;

    public SSesionResult(Player playerCharacter, ESessionResult result, STime time, int collectCoins, int defeatEnemies, int completedWaves)
    {
        name = $"{playerCharacter.GetName()} {DateTime.Now}";
        
        this.playerCharacter = playerCharacter;
        this.result = result;
        this.time = time;
        this.collectCoins = collectCoins;
        this.defeatEnemies = defeatEnemies;
        this.completedWaves = completedWaves;
    }

    public SSesionResult(Player playerCharacter, ESessionResult result, int totalSeconds, int collectCoins, int defeatEnemies, int completedWaves)
    {
        name = $"{playerCharacter.GetName()} {DateTime.Now}";
        
        this.playerCharacter = playerCharacter;
        this.result = result;
        this.time = new STime(totalSeconds);
        this.collectCoins = collectCoins;
        this.defeatEnemies = defeatEnemies;
        this.completedWaves = completedWaves;
    }
}