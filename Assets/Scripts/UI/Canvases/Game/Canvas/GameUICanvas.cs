using UnityEngine;

public class GameUICanvas : MonoBehaviour
{
    [SerializeField] private UI_Gameplay ui_gameplay;
    [SerializeField] private UI_Pause ui_pause;
    [SerializeField] private UI_SessionResultsGame ui_resultsSession;

    public void Initialize(PlayerCharacter playerCharacter)
    {
        ui_gameplay.Initialize(playerCharacter);
        ui_resultsSession.Initialize(ui_gameplay);
        ui_pause.Initialize(playerCharacter.UnitContext, ui_gameplay);

        ui_gameplay.Show();

        ui_pause.Hide();
        ui_resultsSession.Hide();
    }

    public UI_Gameplay UIGameplay => ui_gameplay;
    public UI_Pause UIPause => ui_pause;
    public UI_SessionResultsGame UIResultsSession => ui_resultsSession;
}