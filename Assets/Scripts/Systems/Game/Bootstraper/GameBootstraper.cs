using UnityEngine;

public class GameBootstraper : MonoBehaviour
{
    [SerializeField] private GameUICanvas gameUICanvas;

    [SerializeField] private CameraOrigin cameraOrigin;

    [SerializeField] private WaveSystem waveSystem;
    [SerializeField] private LandSystem landSystem;
    [SerializeField] private ResourceSystem resourceSystem;

    [SerializeField] private TradeZone _tradeZone;
    [SerializeField] private EndGame _endGame;

    private SessionSystem sessionSystem;
    
    private PlayerCharacter playerCharacter;

    private GameInstance _gameInstance;

    private void Start()
    {
        _gameInstance = GameInstance.current;
        
        Time.timeScale = 1f;
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 120;

        InitializePlayer();

        waveSystem.Initialize(_gameInstance.CurrentGameLevel.WaveConfig.Waves, playerCharacter);

        landSystem.Initialize();
        resourceSystem.Initialize(_gameInstance.CurrentGameLevel.Resources, landSystem);

        gameUICanvas.Initialize(playerCharacter, waveSystem);

        _tradeZone.Initialize(_gameInstance, gameUICanvas.UIGameplay.UITrade, gameUICanvas.UIGameplay.UIOffer);

        sessionSystem = new SessionSystem(playerCharacter, gameUICanvas.UIResultsSession, gameUICanvas.UIPause, waveSystem, _tradeZone);

        _endGame.Initialize(waveSystem, sessionSystem, gameUICanvas.UIGameplay.UIOffer);

        sessionSystem.StartSession();
        resourceSystem.SpawnResoure.StartSpawn();
    }

    private void InitializePlayer()
    {
        playerCharacter = Instantiate(
            _gameInstance.GetDataBase().Characters.GetCharacterByName(
                _gameInstance.GetProfile().playerCharacterName
            )
        );
        playerCharacter.transform.position = new Vector3(0f, 1f, 0f);
        playerCharacter.Initialize(gameUICanvas.UIGameplay.UIJoystick); 

        cameraOrigin.Initialize(playerCharacter.transform);
    }
}