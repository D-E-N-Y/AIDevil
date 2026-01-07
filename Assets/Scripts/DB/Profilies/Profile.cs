using System.Collections.Generic;

[System.Serializable]
public class Profile
{
    public string name;
    public string playerCharacterName;
    public List<SSesionResult> sessionResults;
    public BestiarySaveData bestiaryData;

    public Profile()
    {
        name = null;
        playerCharacterName = null;
        sessionResults = new List<SSesionResult>();
        bestiaryData = new BestiarySaveData(new List<string>());
    }

    public Profile(string name)
    {
        this.name = name;
        playerCharacterName = null;
        sessionResults = new List<SSesionResult>();
        bestiaryData = new BestiarySaveData(new List<string>());
    }

    // public Profile(string name, string playerCharacterName, List<SSesionResult> sessionResults, BestiarySaveData? bestiaryData = null)
    // {
    //     this.name = name;
    //     this.playerCharacterName = playerCharacterName;
    //     this.sessionResults = sessionResults;
    //     this.bestiaryData = bestiaryData ?? new BestiarySaveData(new List<string>());
    // }
}