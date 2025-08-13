using System.Collections.Generic;
using UnityEngine;

public class GameBootstraper : MonoBehaviour 
{
    [SerializeField] private GameUICanvas gameUICanvas;

    [SerializeField] private Player player;
    [SerializeField] private CameraOrigin cameraOrigin;

    [SerializeField] private WaveSystem waveSystem;

    private void Start()
    {
        player.Initialize();
        player.SetControlers(gameUICanvas.GetUIFixedJoystick());
        cameraOrigin.Initialize(player.transform);

        waveSystem.Initialize(player);
        waveSystem.StartWave();
    }
}