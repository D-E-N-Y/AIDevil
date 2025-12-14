using System.Collections.Generic;

[System.Serializable]
public struct Profile
{
    public string name;
    public string playerCharacterName;
    public List<SSesionResult> sessionResults;
    public BestiarySaveData bestiaryData;

    public Profile(string name, string playerCharacterName, List<SSesionResult> sessionResults, BestiarySaveData? bestiaryData = null)
    {
        this.name = name;
        this.playerCharacterName = playerCharacterName;
        this.sessionResults = sessionResults;
        this.bestiaryData = bestiaryData ?? new BestiarySaveData(new List<string>());
    }
}