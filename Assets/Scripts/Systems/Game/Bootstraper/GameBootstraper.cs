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
        player.SetControlers(gameUICanvas.GetUIFixedJoystick(), gameUICanvas.GetMelleAttackButton());
        player.Initialize();
        cameraOrigin.Initialize(player.transform);

        waveSystem.Initialize(player);
        waveSystem.StartWave();
    }
}