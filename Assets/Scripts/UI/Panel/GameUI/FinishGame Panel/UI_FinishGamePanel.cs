using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_FinishGamePanel : UI_Panel
{
    [SerializeField] private Button ui_restartButton;

    public void Initialize(Player player)
    {
        Hide();
        player.onDead += ShowResult;
        ui_restartButton.onClick.AddListener(() => SceneManager.LoadScene("GameScene"));
    }

    private void ShowResult(IHealth _unit)
    {
        Show();
        Time.timeScale = 0f;
    }
}
