using UnityEngine;

public class GameBootstraper : MonoBehaviour
{
    [SerializeField] private GameUICanvas gameUICanvas;

    [SerializeField] private CameraOrigin cameraOrigin;

    [SerializeField] private SessionSystem sessionSystem;
    [SerializeField] private WaveSystem waveSystem;

    private Player playerCharacter;

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

        sessionSystem.Initialize(playerCharacter, gameUICanvas.GetUIResultsSession(), waveSystem);
        waveSystem.Initialize(playerCharacter);

        sessionSystem.StartSession();
    }

    private void InitializePlayer()
    {
        playerCharacter = Instantiate(_gameInstance.GetPlayerCharacter());
        playerCharacter.transform.position = new Vector3(0f, 1f, 0f);
        
        playerCharacter.SetControlers(gameUICanvas.GetUIFixedJoystick());
        
        playerCharacter.Initialize(); 
    }
}