public class GameLevelsManager
{
    private GameLevel _currentGameLevel;
    public GameLevel CurrentGameLevel => _currentGameLevel;

    private DB_GameLevels _gameLevels; 

    public GameLevelsManager(DB_GameLevels gameLevels)
    {
        _gameLevels = gameLevels;

        _currentGameLevel = null;
    }

    public void SetCurrentGameLevel(string nameGameLevel)
    {
        _currentGameLevel = _gameLevels.GetGameLevelByName(nameGameLevel);
    }

    public void SetCurrentGameLevel(GameLevel gameLevel)
    {
        _currentGameLevel = gameLevel;
    }
}