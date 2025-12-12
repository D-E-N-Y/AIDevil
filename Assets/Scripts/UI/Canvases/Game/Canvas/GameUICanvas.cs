using UnityEngine;

public class GameUICanvas : MonoBehaviour
{
    [SerializeField] private FixedJoystick ui_joystick;
    [SerializeField] private UI_AttackRangeContainer ui_attackRangeContainer;
    [SerializeField] private UI_AttackMeleeContainer ui_attackMeleeContainer;
    [SerializeField] private UI_Wave ui_wave;

    [SerializeField] private UI_Panel ui_blackout;
    [SerializeField] private UI_PauseMenu ui_pauseMenu;
    [SerializeField] private UI_SessionResultsGame ui_resultsSession;

    public void Initialize(Player playerCharacter, WaveSystem waveSystem)
    {
        ui_attackRangeContainer.Initialize(playerCharacter);
        ui_attackMeleeContainer.Initialize(playerCharacter);
        ui_wave.Initialize(waveSystem);

        ui_resultsSession.Initialize(ui_blackout);
        ui_pauseMenu.Initialize(ui_blackout);
    }

    public FixedJoystick GetUIFixedJoystick() => ui_joystick;
    public UI_SessionResultsGame GetUIResultsSession() => ui_resultsSession;
}