using System.Collections.Generic;

[System.Serializable]
public struct Profile
{
    public string name;
    public string playerCharacterName;
    public List<SSesionResult> sessionResults;

    public Profile(string name, string playerCharacterName, List<SSesionResult> sessionResults)
    {
        this.name = name;
        this.playerCharacterName = playerCharacterName;
        this.sessionResults = sessionResults;
    }
}