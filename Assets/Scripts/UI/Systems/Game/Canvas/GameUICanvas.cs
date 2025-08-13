using UnityEngine;
using UnityEngine.UI;

public class GameUICanvas : MonoBehaviour
{
    [SerializeField] private FixedJoystick uiJoystick;
    [SerializeField] private Button melleAttackButton;

    public FixedJoystick GetUIFixedJoystick() => uiJoystick;
    public Button GetMelleAttackButton() => melleAttackButton;
}