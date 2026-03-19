using System;
using UnityEngine;

public class MainMenuBootstraper : MonoBehaviour
{
    [SerializeField] private UI_MainMenuCanvas ui_mainMenuCanvas;

    private GameInstance _gameInstance;

    private void Start()
    {
        _gameInstance = GameInstance.current;
        
        ui_mainMenuCanvas.Initialize(_gameInstance);

        if(!_gameInstance.IsValidProfile())
        {
            ui_mainMenuCanvas.ShowProfiliesMenu();
        }

        foreach (ResourceType resource in Enum.GetValues(typeof(ResourceType)))
        {
            if (_gameInstance.ProfileWallet.Resources.ContainsKey(resource))
            {
                Debug.Log($"{resource} {_gameInstance.ProfileWallet.Resources[resource]}");
            }
        }
    }

    private void OnDestroy()
    {
        _gameInstance.ClearSubscriptions();
    }
}
