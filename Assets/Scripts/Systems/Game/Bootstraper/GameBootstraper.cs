using System.Collections.Generic;
using UnityEngine;

public class GameBootstraper : MonoBehaviour 
{
    [SerializeField] private GameUICanvas gameUICanvas;

    [SerializeField] private P_User player;
    [SerializeField] private CameraOrigin cameraOrigin;

    [SerializeField] private List<EM_Melee> meleeEnemies;

    private void Start()
    {
        player.Initialize();
        player.SetControlers(gameUICanvas.GetUIFixedJoystick());
        cameraOrigin.Initialize(player.transform);

        foreach (EM_Melee enemy in meleeEnemies)
        {
            enemy.Initialize();
            enemy.SetPlayerTarget(player);
        }
    }
}