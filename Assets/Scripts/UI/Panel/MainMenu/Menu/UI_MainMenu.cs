using UnityEngine;
using UnityEngine.UI;

// [System.Serializable]
// public struct UITab
// {
//     public Button button;
//     public UI_Panel panel;
// }

public class UI_MainMenu : UI_Panel
{
    [SerializeField] private Button ui_playButton;
    [SerializeField] private Button ui_settingsButton;
    [SerializeField] private Button ui_sessionResultsButton;
    [SerializeField] private Button ui_quitButton;

    public void Initialize(UI_CharactersMenu ui_charactesPanel, UI_SessionResultsMenu ui_sessionResultsMenu)
    {
        ui_playButton.onClick.RemoveAllListeners();
        ui_playButton.onClick.AddListener(() => ui_charactesPanel.Show());

        // ui_settingsButton.onClick.RemoveAllListeners();
        // ui_settingsButton.onClick.AddListener(() => Application.Quit());
        
        ui_sessionResultsButton.onClick.RemoveAllListeners();
        ui_sessionResultsButton.onClick.AddListener(() => ui_sessionResultsMenu.Show());
        ui_sessionResultsButton.interactable = GameInstance.current.DBSessionResults().HasRecords();

        ui_quitButton.onClick.RemoveAllListeners();
        ui_quitButton.onClick.AddListener(() => Application.Quit());
    }
}
