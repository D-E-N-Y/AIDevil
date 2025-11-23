using UnityEngine;
using UnityEngine.SceneManagement;

public class MainBootstraper : MonoBehaviour
{
    [SerializeField] private GameInstance gameInstance;

    private void Start()
    {
        gameInstance.Initialize();

        SceneManager.LoadScene("MainMenuScene");
    }
}