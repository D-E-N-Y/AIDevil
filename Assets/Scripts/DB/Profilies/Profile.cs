public struct Profile
{
    public string name;
    public Player playerCharacter;
    public DB_Characters db_characters;
    public DB_SessionResults db_sessionResults;

    public Profile(string name, Player playerCharacter, DB_Characters db_characters, DB_SessionResults db_sessionResults)
    {
        this.name = name;
        this.playerCharacter = playerCharacter;
        this.db_characters = db_characters;
        this.db_sessionResults = db_sessionResults;
    }
}