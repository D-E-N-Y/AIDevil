using UnityEngine;

public class GameUICanvas : MonoBehaviour
{
    [SerializeField] private UI_Gameplay ui_gameplay;
    [SerializeField] private UI_Pause ui_pause;
    [SerializeField] private UI_SessionResultsGame ui_resultsSession;

    [SerializeField] private UI_Panel ui_blackout;

    public void Initialize(PlayerCharacter playerCharacter, WaveSystem waveSystem)
    {
        ui_gameplay.Initialize(playerCharacter, waveSystem);
        ui_resultsSession.Initialize(ui_blackout);
        ui_pause.Initialize(playerCharacter.GetItemContext(), ui_gameplay);

        ui_gameplay.Show();
        ui_pause.Hide();
        ui_resultsSession.Hide();
    }

    public UI_Gameplay UIGameplay => ui_gameplay;
    public UI_Pause UIPause => ui_pause;
    public UI_SessionResultsGame UIResultsSession => ui_resultsSession;
}