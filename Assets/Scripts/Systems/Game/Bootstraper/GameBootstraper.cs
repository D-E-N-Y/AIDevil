using UnityEngine;

public class GameBootstraper : MonoBehaviour 
{
    [SerializeField] private GameUICanvas gameUICanvas;

    [SerializeField] private Player player;
    [SerializeField] private CameraOrigin cameraOrigin;

    [SerializeField] private WaveSystem waveSystem;

    private void Start()
    {
        Time.timeScale = 1f;
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 120;

        player.SetControlers(gameUICanvas.GetUIFixedJoystick(), gameUICanvas.GetMelleAttackButton());
        player.Initialize();

        cameraOrigin.Initialize(player.transform);

        gameUICanvas.Initialize(player);

        waveSystem.Initialize(player);
        waveSystem.StartWave();
    }
}