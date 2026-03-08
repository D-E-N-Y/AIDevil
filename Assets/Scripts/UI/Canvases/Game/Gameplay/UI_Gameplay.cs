using UnityEngine;
using UnityEngine.UI;

public class UI_Gameplay : UI_Panel 
{
    [SerializeField] private FixedJoystick ui_joystick;

    [SerializeField] private UI_AttackRangeContainer ui_attackRangeContainer;
    [SerializeField] private UI_AttackMeleeContainer ui_attackMeleeContainer;

    [SerializeField] private UI_HealthBar ui_healthBar;
    [SerializeField] private UI_MoneyAmount ui_moneyAmount;

    [SerializeField] private UI_Wave ui_wave;

    [SerializeField] private UI_Trade ui_trade;

    [SerializeField] private UI_Offer ui_offer;

    [SerializeField] private Button ui_pauseButton;

    public void Initialize(PlayerCharacter playerCharacter, WaveSystem waveSystem)
    {
        ui_attackRangeContainer.Initialize(playerCharacter);
        ui_attackMeleeContainer.Initialize(playerCharacter);
        ui_healthBar.Initialize(playerCharacter.GetHealth());
        ui_moneyAmount.Initialize(playerCharacter.GetWallet());
        ui_wave.Initialize(waveSystem);

        ui_trade.Hide();

        ui_offer.Hide();
    }

    public FixedJoystick UIJoystick => ui_joystick;
    
    public UI_AttackRangeContainer UIAttackRangeContainer => ui_attackRangeContainer;
    public UI_AttackMeleeContainer UIAttackMeleeContainer => ui_attackMeleeContainer;

    public UI_MoneyAmount UIMoneyAmount => ui_moneyAmount;

    public UI_Wave UIWave => ui_wave;

    public UI_Trade UITrade => ui_trade;
    public UI_Offer UIOffer => ui_offer;

    public Button UIPauseButton => ui_pauseButton;
}