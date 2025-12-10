using UnityEngine;

public class GameUICanvas : MonoBehaviour
{
    [SerializeField] private FixedJoystick ui_joystick;
    [SerializeField] private UI_AttackRangeContainer ui_attackRangeContainer;
    [SerializeField] private UI_AttackMeleeContainer ui_attackMeleeContainer;

    [SerializeField] private UI_Panel ui_blackout;
    [SerializeField] private UI_PauseMenu ui_pauseMenu;
    [SerializeField] private UI_ResultsSession ui_resultsSession;

    public void Initialize(Player playerCharacter)
    {
        ui_attackRangeContainer.Initialize(playerCharacter);
        ui_attackMeleeContainer.Initialize(playerCharacter);

        ui_resultsSession.Initialize(ui_blackout);
        ui_pauseMenu.Initialize(ui_blackout);
    }

    public FixedJoystick GetUIFixedJoystick() => ui_joystick;
    public UI_ResultsSession GetUIResultsSession() => ui_resultsSession;
}