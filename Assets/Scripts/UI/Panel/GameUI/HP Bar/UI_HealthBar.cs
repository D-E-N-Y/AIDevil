using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_HealthBar : UI_Panel 
{
    [SerializeField] private Image ui_healthBarFill;

    [SerializeField] private TextMeshProUGUI ui_currentHealthText;
    [SerializeField] private TextMeshProUGUI ui_maxHealthText;

    private UnitHealth _unitHealth;

    public void Initialize(UnitHealth unitHealth)
    {
        _unitHealth = unitHealth;
        _unitHealth.OnHpChanged += UpdateHealthBar;

        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        ui_currentHealthText.text = _unitHealth.CurrentHP.ToString();
        ui_maxHealthText.text = _unitHealth.MaxHP.ToString();

        float fillAmount = (float)_unitHealth.CurrentHP / (float)_unitHealth.MaxHP;
        ui_healthBarFill.fillAmount = fillAmount;
    }
}