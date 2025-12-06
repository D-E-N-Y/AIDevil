using UnityEngine;

public class GameBootstraper : MonoBehaviour
{
    [SerializeField] private GameUICanvas gameUICanvas;

    [SerializeField] private CameraOrigin cameraOrigin;

    [SerializeField] private WaveSystem waveSystem;

    private Player player;

    private void Start()
    {
        Time.timeScale = 1f;
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 120;

        InitializePlayer();

        cameraOrigin.Initialize(player.transform);

        gameUICanvas.Initialize(player);

        waveSystem.Initialize(player);
        waveSystem.StartWave();
    }

    private void InitializePlayer()
    {
        player = Instantiate(GameInstance.current.GetPlayer());
        player.transform.position = new Vector3(0f, 1f, 0f);
        
        player.SetControlers(gameUICanvas.GetUIFixedJoystick());
        
        player.Initialize(); 
    }
}