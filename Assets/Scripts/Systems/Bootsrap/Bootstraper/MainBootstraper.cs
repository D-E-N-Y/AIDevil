using UnityEngine;
using UnityEngine.SceneManagement;

public class MainBootstraper : MonoBehaviour
{
    [SerializeField] private SaveLoadSystem saveLoadSystem;
    [SerializeField] private GameInstance gameInstance;

    private void Start()
    {
        saveLoadSystem.Initialize();
        gameInstance.Initialize(saveLoadSystem);

        SceneManager.LoadScene("MainMenuScene");
    }
}