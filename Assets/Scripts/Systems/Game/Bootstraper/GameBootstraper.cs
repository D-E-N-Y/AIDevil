using UnityEngine;

public class GameBootstraper : MonoBehaviour 
{
    [SerializeField] private GameUICanvas gameUICanvas;

    [SerializeField] private Player player;
    [SerializeField] private CameraOrigin cameraOrigin;

    private void Start()
    {
        player.Initialize(gameUICanvas.GetUIFixedJoystick());
        cameraOrigin.Initialize(player.transform);
    }
}