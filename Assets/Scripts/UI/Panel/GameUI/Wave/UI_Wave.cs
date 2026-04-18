using TMPro;
using UnityEngine;

public class UI_Wave : UI_Panel 
{
    [SerializeField] private TextMeshProUGUI ui_waveNumberText;
    [SerializeField] private TextMeshProUGUI ui_countEnemiesText;

    [SerializeField] private TextMeshProUGUI ui_timerToNextWaveText;

    public void Initialize(WaveSystem waveSystem)
    {
        waveSystem.updateNumberWave += UpdateWave;
        waveSystem.EnemyManager.updateCountEnemies += UpdateEnemies;
    }

    private void UpdateWave(int number) => ui_waveNumberText.text = number.ToString();
    private void UpdateEnemies(int count) => ui_countEnemiesText.text = count.ToString();
}