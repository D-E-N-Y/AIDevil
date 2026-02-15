using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Stats : UI_Panel 
{
    [SerializeField] private Button ui_backButton;

    [SerializeField] private TextMeshProUGUI ui_maxHPText;
    [SerializeField] private TextMeshProUGUI ui_baseMoveSpeedText;
    [SerializeField] private TextMeshProUGUI ui_moveSpeedModifierText;
    [SerializeField] private TextMeshProUGUI ui_armorText;
    [SerializeField] private TextMeshProUGUI ui_damageModifierText;
    [SerializeField] private TextMeshProUGUI ui_speedAttackModifierText;
    [SerializeField] private TextMeshProUGUI ui_criticalDamageChanceText;
    [SerializeField] private TextMeshProUGUI ui_criticalDamageModifierText;
    [SerializeField] private TextMeshProUGUI ui_multiattackChanceText;
    [SerializeField] private TextMeshProUGUI ui_areaModifierText;
    [SerializeField] private TextMeshProUGUI ui_dodgeChanceText;
    [SerializeField] private TextMeshProUGUI ui_pickUpRangeModifierText;
    [SerializeField] private TextMeshProUGUI ui_moneyModifierText;

    private PlayerCharacterStats _stats;

    public void Initialize(PlayerCharacterStats stats, UI_PauseMenu ui_pauseMenu)
    {
        _stats = stats;

        ui_backButton.onClick.RemoveAllListeners();
        ui_backButton.onClick.AddListener(() => {
            ui_pauseMenu.Show();
            Hide();
        });
    }
    
    public void SetData()
    {
        ui_maxHPText.text = _stats.MaxHP.ToString();
        ui_baseMoveSpeedText.text = _stats.BaseMoveSpeed.ToString();
        ui_moveSpeedModifierText.text = (_stats.MoveSpeedModifier * 100f).ToString();
        ui_armorText.text = _stats.Armor.ToString();
        ui_damageModifierText.text = (_stats.DamageModifier * 100f).ToString();
        ui_speedAttackModifierText.text = (_stats.SpeedAttackModifier * 100f).ToString();
        ui_criticalDamageChanceText.text = (_stats.CriticalDamageChance * 100f).ToString();
        ui_criticalDamageModifierText.text = (_stats.CriticalDamageModifier * 100f).ToString();
        ui_multiattackChanceText.text = (_stats.MultiattackChance * 100f).ToString();
        ui_areaModifierText.text = (_stats.AreaModifier * 100f).ToString();
        ui_dodgeChanceText.text = (_stats.DodgeChance * 100f).ToString();
        ui_pickUpRangeModifierText.text = (_stats.PickUpRangeModifier * 100f).ToString();
        ui_moneyModifierText.text = (_stats.MoneyModifier * 100f).ToString();
    }

    public override void Show()
    {
        base.Show();
        SetData();
    }
}