using UnityEngine;

public class MainMenuBootstraper : MonoBehaviour
{
    [SerializeField] private UI_MainMenuCanvas uI_mainMenuCanvas;

    private void Start()
    {
        uI_mainMenuCanvas.Initialize();
    }
}
