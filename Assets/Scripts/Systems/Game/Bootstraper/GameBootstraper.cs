using System.Collections.Generic;
using UnityEngine;

public class GameBootstraper : MonoBehaviour 
{
    [SerializeField] private GameUICanvas gameUICanvas;

    [SerializeField] private Player player;
    [SerializeField] private CameraOrigin cameraOrigin;

    [SerializeField] private List<EMM_Melee> meleeEnemies;

    private void Start()
    {
        player.Initialize(gameUICanvas.GetUIFixedJoystick());
        cameraOrigin.Initialize(player.transform);

        foreach (EMM_Melee enemy in meleeEnemies)
        {
            enemy.Initialize();
            enemy.SetTarget(player.transform);
        }
    }
}