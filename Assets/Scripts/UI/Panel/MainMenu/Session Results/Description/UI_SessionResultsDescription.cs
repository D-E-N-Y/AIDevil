using TMPro;
using UnityEngine;

public class UI_SessionResultsDescription : UI_Panel 
{
    [SerializeField] private TextMeshProUGUI ui_playerText;
    [SerializeField] private TextMeshProUGUI ui_resultText;
    [SerializeField] private TextMeshProUGUI ui_hoursText;
    [SerializeField] private TextMeshProUGUI ui_minutesText;
    [SerializeField] private TextMeshProUGUI ui_secondsText;
    [SerializeField] private TextMeshProUGUI ui_collectCoins;
    [SerializeField] private TextMeshProUGUI ui_defeatEnemies;
    [SerializeField] private TextMeshProUGUI ui_completedWaves;

    public void SetResult(SSesionResult result)
    {
        ui_playerText.text = result.namePlayerCharacter;
        ui_resultText.text = result.result.ToString();
        ui_hoursText.text = result.time.hours.ToString();
        ui_minutesText.text = CorrectTimeFormat(result.time.minutes);
        ui_secondsText.text = CorrectTimeFormat(result.time.seconds);
        ui_collectCoins.text = result.collectCoins.ToString();
        ui_defeatEnemies.text = result.defeatEnemies.ToString();
        ui_completedWaves.text = result.completedWaves.ToString();
    }

    private string CorrectTimeFormat(int value)
    {
        string _correctForm = "";
        
        if(value < 10)
        {
            _correctForm = $"0{value}";
        }
        else
        {
            _correctForm = value.ToString();
        }

        return _correctForm;
    }
}