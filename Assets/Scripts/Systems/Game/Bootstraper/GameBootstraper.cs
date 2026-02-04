using UnityEngine;

public class GameBootstraper : MonoBehaviour
{
    [SerializeField] private GameUICanvas gameUICanvas;

    [SerializeField] private CameraOrigin cameraOrigin;

    [SerializeField] private WaveSystem waveSystem;
    [SerializeField] private TradeZone _tradeZone;
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

        cameraOrigin.Initialize(playerCharacter.transform);

        gameUICanvas.Initialize(playerCharacter, waveSystem);
        
        waveSystem.Initialize(playerCharacter);
        _tradeZone.Initialize(_gameInstance);

        sessionSystem = new SessionSystem(playerCharacter, gameUICanvas.GetUIResultsSession(), waveSystem, _tradeZone);

        sessionSystem.StartSession();
    }

    private void InitializePlayer()
    {
        playerCharacter = Instantiate(
            _gameInstance.GetDataBase().Characters.GetCharacterByName(
                _gameInstance.GetProfile().playerCharacterName
            )
        );
        playerCharacter.transform.position = new Vector3(0f, 1f, 0f);
        playerCharacter.Initialize(gameUICanvas.GetUIFixedJoystick()); 
    }
}