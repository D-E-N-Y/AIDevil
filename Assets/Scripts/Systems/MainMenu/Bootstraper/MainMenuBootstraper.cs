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
    }

    private void OnDestroy()
    {
        _gameInstance.ClearSubscriptions();
    }
}
