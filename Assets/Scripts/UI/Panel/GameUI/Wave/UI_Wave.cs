using TMPro;
using UnityEngine;

public class UI_Wave : UI_Panel 
{
    [SerializeField] private TextMeshProUGUI ui_waveNumberText;
    [SerializeField] private TextMeshProUGUI ui_countEnemiesText;

    [SerializeField] private TextMeshProUGUI ui_timerToNextWaveText;

    private WaveSystem _waveSystem;

    public void Initialize(WaveSystem waveSystem)
    {
        _waveSystem = waveSystem;

        _waveSystem.updateNumberWave += UpdateWave;
        _waveSystem.EnemyController.updateCountEnemies += UpdateEnemies;
    }

    private void UpdateWave(int number) => ui_waveNumberText.text = number.ToString();
    private void UpdateEnemies(int count) => ui_countEnemiesText.text = count.ToString();
}