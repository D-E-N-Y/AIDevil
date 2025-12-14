using UnityEngine;
using UnityEngine.SceneManagement;

public class MainBootstraper : MonoBehaviour
{
    [SerializeField] private SaveLoadSystem saveLoadSystem;
    [SerializeField] private DataBase dataBase;
    [SerializeField] private GameInstance gameInstance;

    private void Start()
    {
        saveLoadSystem.Initialize();
        dataBase.Initialize();
        gameInstance.Initialize(saveLoadSystem, dataBase);

        SceneManager.LoadScene("MainMenuScene");
    }
}