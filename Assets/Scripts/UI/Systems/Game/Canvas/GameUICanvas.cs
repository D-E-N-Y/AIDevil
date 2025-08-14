using UnityEngine;
using UnityEngine.UI;

public class GameUICanvas : MonoBehaviour
{
    [SerializeField] private FixedJoystick uiJoystick;
    [SerializeField] private Button melleAttackButton;

    [SerializeField] private UI_FinishGamePanel ui_finishGamePanel;

    public void Initialize(Player player)
    {
        ui_finishGamePanel.Initialize(player);
    }

    public FixedJoystick GetUIFixedJoystick() => uiJoystick;
    public Button GetMelleAttackButton() => melleAttackButton;
}