using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_SessionResultsGame : UI_Panel
{
    [SerializeField] private UI_SessionResultsDescription ui_sessionResultsDescription;
    
    [SerializeField] private Button ui_exitButton;
    [SerializeField] private Button ui_restartButton;

    private UI_Gameplay _ui_gamePlay;

    public void Initialize(UI_Gameplay ui_gamePlay)
    {
        _ui_gamePlay = ui_gamePlay;
        Hide();

        ui_restartButton.onClick.RemoveAllListeners();
        ui_restartButton.onClick.AddListener(() => SceneManager.LoadScene(GameInstance.current.CurrentGameLevel.Name)); 
        
        ui_exitButton.onClick.RemoveAllListeners();
        ui_exitButton.onClick.AddListener(() => SceneManager.LoadScene("MainMenuScene"));
    }

    public override void Hide()
    {
        base.Hide();
        _ui_gamePlay.Show();

        Time.timeScale = 1f;
    }

    public override void Show()
    {
        base.Show();
        _ui_gamePlay.Hide();

        Time.timeScale = 0f;
    }

    public void SetResult(SSesionResult result)
    {
        ui_sessionResultsDescription.SetResult(result);
    }
}
