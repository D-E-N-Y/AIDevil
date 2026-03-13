using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Stat : UI_Panel 
{
    [SerializeField] private Image ui_icon;
    [SerializeField] private TextMeshProUGUI ui_nameText;
    [SerializeField] private TextMeshProUGUI ui_valueText;
    [SerializeField] private TextMeshProUGUI ui_percentageText;

    private float _percentageModifier;

    public void Initialize(StatIcons.StatIcon statIcon)
    {
        SetPercentage(statIcon);
        
        ui_icon.sprite = statIcon.icon;
        ui_icon.color = statIcon.color;
        
        ui_nameText.text = statIcon.stat.ToString();
    }

    public void SetValue(float value)
    {
        ui_valueText.text = (value * _percentageModifier).ToString();
    }

    private void SetPercentage(StatIcons.StatIcon statIcon)
    {
        if (statIcon.stat == StatType.MaxHP ||
            statIcon.stat == StatType.BaseMoveSpeed ||
            statIcon.stat == StatType.Armor ||
            statIcon.stat == StatType.DropMoney)
        {
            ui_percentageText.gameObject.SetActive(false);
            _percentageModifier = 1f;
        }
        else
        {
            ui_percentageText.gameObject.SetActive(true);
            _percentageModifier = 100f;
        }
    }
}