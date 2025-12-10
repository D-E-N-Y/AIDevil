using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_ResultsSession : UI_Panel
{
    [SerializeField] private TextMeshProUGUI ui_playerText;
    [SerializeField] private TextMeshProUGUI ui_resultText;
    [SerializeField] private TextMeshProUGUI ui_hoursText;
    [SerializeField] private TextMeshProUGUI ui_minutesText;
    [SerializeField] private TextMeshProUGUI ui_secondsText;
    [SerializeField] private TextMeshProUGUI ui_collectCoins;
    [SerializeField] private TextMeshProUGUI ui_defeatEnemies;
    [SerializeField] private TextMeshProUGUI ui_completedWaves;
    
    [SerializeField] private Button ui_exitButton;
    [SerializeField] private Button ui_restartButton;

    private UI_Panel _ui_blackout;

    private SSesionResult _result;

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
        _result = result;

        ui_playerText.text = _result.playerCharacter.GetName();
        ui_resultText.text = _result.result.ToString();
        ui_hoursText.text = _result.time.hours.ToString();
        ui_minutesText.text = _result.time.minutes.ToString();
        ui_secondsText.text = _result.time.seconds.ToString();
        ui_collectCoins.text = _result.collectCoins.ToString();
        ui_defeatEnemies.text = _result.defeatEnemies.ToString();
        ui_completedWaves.text = _result.completedWaves.ToString();
    }
}
