using System;
using System.Collections.Generic;

[Serializable]
public struct SSesionResult
{
    public string name;
    public string namePlayerCharacter;
    public ESessionResult result;
    public STime time;
    public Dictionary<ResourceType, int> collectResources;
    public int defeatEnemies;
    public int completedWaves;

    public SSesionResult(string namePlayerCharacter, ESessionResult result, STime time, Dictionary<ResourceType, int> collectResources, int defeatEnemies, int completedWaves)
    {
        name = $"{namePlayerCharacter} {DateTime.Now}";
        
        this.namePlayerCharacter = namePlayerCharacter;
        this.result = result;
        this.time = time;
        this.collectResources = collectResources;
        this.defeatEnemies = defeatEnemies;
        this.completedWaves = completedWaves;
    }

    public SSesionResult(string namePlayerCharacter, ESessionResult result, int totalSeconds, Dictionary<ResourceType, int> collectResources, int defeatEnemies, int completedWaves)
    {
        name = $"{namePlayerCharacter} {DateTime.Now}";
        
        this.namePlayerCharacter = namePlayerCharacter;
        this.result = result;
        this.time = new STime(totalSeconds);
        this.collectResources = collectResources;
        this.defeatEnemies = defeatEnemies;
        this.completedWaves = completedWaves;
    }
}