using UnityEngine;
using UnityEngine.UI;

public class GameUICanvas : MonoBehaviour
{
    [SerializeField] private FixedJoystick ui_joystick;
    [SerializeField] private UI_AttackRangeContainer ui_attackRangeContainer;
    [SerializeField] private UI_AttackMeleeContainer ui_attackMeleeContainer;

    [SerializeField] private UI_FinishGamePanel ui_finishGamePanel;

    public void Initialize(Player playerCharacter)
    {
        ui_attackRangeContainer.Initialize(playerCharacter);
        ui_attackMeleeContainer.Initialize(playerCharacter);

        ui_finishGamePanel.Initialize(playerCharacter);
    }

    public FixedJoystick GetUIFixedJoystick() => ui_joystick;
}