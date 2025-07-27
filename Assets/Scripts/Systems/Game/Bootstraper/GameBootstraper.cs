using UnityEngine;

public class GameBootstraper : MonoBehaviour 
{
    [SerializeField] private GameUICanvas gameUICanvas;
    [SerializeField] private Player player;

    private void Start()
    {
        player.Initialize(gameUICanvas.GetUIFixedJoystick());
    }
}