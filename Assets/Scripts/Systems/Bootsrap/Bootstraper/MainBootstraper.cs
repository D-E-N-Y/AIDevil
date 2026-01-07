using UnityEngine;
using UnityEngine.SceneManagement;

public class MainBootstraper : MonoBehaviour
{
    [SerializeField] private DataBase dataBase;
    [SerializeField] private GameInstance gameInstance;

    private void Start()
    {
        dataBase.Initialize();
        gameInstance.Initialize(dataBase);

        SceneManager.LoadScene("MainMenuScene");
    }
}