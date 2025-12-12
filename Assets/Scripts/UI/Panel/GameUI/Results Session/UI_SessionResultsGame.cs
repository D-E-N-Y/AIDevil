using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_SessionResultsGame : UI_Panel
{
    [SerializeField] private UI_SessionResultsDescription ui_sessionResultsDescription;
    
    [SerializeField] private Button ui_exitButton;
    [SerializeField] private Button ui_restartButton;

    private UI_Panel _ui_blackout;

    public void Initialize(UI_Panel ui_blackout)
    {
        _ui_blackout = ui_blackout;
        Hide();

        ui_restartButton.onClick.RemoveAllListeners();
        ui_restartButton.onClick.AddListener(() => SceneManager.LoadScene("GameScene"));
        
        ui_exitButton.onClick.RemoveAllListeners();
        ui_exitButton.onClick.AddListener(() => SceneManager.LoadScene("MainMenuScene"));
    }

    public override void Hide()
    {
        base.Hide();
        _ui_blackout.Hide();

        Time.timeScale = 1f;
    }

    public override void Show()
    {
        base.Show();
        _ui_blackout.Show();

        Time.timeScale = 0f;
    }

    public void SetResult(SSesionResult result)
    {
        ui_sessionResultsDescription.SetResult(result);
    }
}
