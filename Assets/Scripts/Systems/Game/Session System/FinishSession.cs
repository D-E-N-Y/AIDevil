public class FinishSession
{
    private SSesionResult _sesionResult;
    private GameInstance _gameInstance;
    private UI_SessionResultsGame _ui_sessionResultsGame;

    public FinishSession(GameInstance gameInstance, UI_SessionResultsGame ui_sessionResultsGame)
    {
        _gameInstance = gameInstance;
        _ui_sessionResultsGame = ui_sessionResultsGame;

        _sesionResult = new SSesionResult();
        _sesionResult.result = ESessionResult.LOSE; 
    }

    public void Finish()
    {
        _gameInstance.ProfileManager.CurrentProfile.SessionResultsProgress.AddSessionResult(_sesionResult);
        _gameInstance.ProfileManager.CurrentProfile.Wallet.AddResources(_sesionResult.collectResources);
    
        _ui_sessionResultsGame.SetResult(_sesionResult);
        _ui_sessionResultsGame.Show();
    }

    public void SetResult(SSesionResult result)
    {
        _sesionResult.name = result.name;
        _sesionResult.namePlayerCharacter = result.namePlayerCharacter;
        _sesionResult.time = result.time;
        _sesionResult.collectResources = result.collectResources;
        _sesionResult.defeatEnemies = result.defeatEnemies;
        _sesionResult.completedWaves = result.completedWaves;
    }

    public void Win()
    {
        _sesionResult.result = ESessionResult.WIN;

        string levelID = _gameInstance.GameLevelsManager.CurrentGameLevel.ID;
        _gameInstance.ProfileManager.CurrentProfile.GameLevelsProgress.AddGameLevel(levelID);
    }

    public void Lose()
    {
        _sesionResult.result = ESessionResult.LOSE;
    }
}