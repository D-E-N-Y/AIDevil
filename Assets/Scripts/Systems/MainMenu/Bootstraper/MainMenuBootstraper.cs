using UnityEngine;

public class MainMenuBootstraper : MonoBehaviour
{
    [SerializeField] private UI_MainMenuCanvas ui_mainMenuCanvas;

    private GameInstance _gameInstance;

    private void Start()
    {
        _gameInstance = GameInstance.current;
        
        ui_mainMenuCanvas.Initialize(_gameInstance);

        if(!_gameInstance.ProfileManager.IsValidProfile())
        {
            ui_mainMenuCanvas.ShowProfiliesMenu();
        }

        _gameInstance.AudioSystem.Music.PlayClip("Infiltrated");
    }

    private void OnDestroy()
    {
        _gameInstance.ProfileManager.ClearSubscriptions();
    }
}
