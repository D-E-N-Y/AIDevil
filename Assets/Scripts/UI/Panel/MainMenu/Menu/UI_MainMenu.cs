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
    [SerializeField] private Button ui_quitButton;

    public void Initialize(UI_CharactersMenu ui_charactesPanel)
    {
        ui_playButton.onClick.RemoveAllListeners();
        ui_playButton.onClick.AddListener(() => ui_charactesPanel.Show());

        // ui_settingsButton.onClick.RemoveAllListeners();
        // ui_settingsButton.onClick.AddListener(() => Application.Quit());

        ui_quitButton.onClick.RemoveAllListeners();
        ui_quitButton.onClick.AddListener(() => Application.Quit());
    }
}
