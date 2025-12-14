[System.Serializable]
public struct Profile
{
    public string name;
    public string playerCharacterName;
    public DB_SessionResults db_sessionResults;

    public Profile(string name, string playerCharacterName, DB_SessionResults db_sessionResults)
    {
        this.name = name;
        this.playerCharacterName = playerCharacterName;
        this.db_sessionResults = db_sessionResults;
    }
}