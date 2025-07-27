using UnityEngine;

public class GameUICanvas : MonoBehaviour
{
    [SerializeField] private FixedJoystick uiJoystick;

    public FixedJoystick GetUIFixedJoystick() => uiJoystick;
}