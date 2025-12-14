using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_MainMenu : UI_Panel
{
    [SerializeField] private Button ui_profiliesButton;
    [SerializeField] private TextMeshProUGUI ui_nameProfileText;

    [SerializeField] private Button ui_playButton;
    [SerializeField] private Button ui_settingsButton;
    [SerializeField] private Button ui_sessionResultsButton;
    [SerializeField] private Button ui_bestiaryButton;
    [SerializeField] private Button ui_quitButton;

    private GameInstance _gameInstance;

    public void Initialize(GameInstance gameInstance, UI_ProfiliesMenu ui_profiliesMenu, UI_CharactersMenu ui_charactesPanel, UI_SessionResultsMenu ui_sessionResultsMenu, UI_BestiaryMenu ui_bestiaryMenu)
    {
        _gameInstance = gameInstance;
        
        UpdateData(ui_profiliesMenu, ui_charactesPanel, ui_sessionResultsMenu, ui_bestiaryMenu);
    }

    public void UpdateData(UI_ProfiliesMenu ui_profiliesMenu, UI_CharactersMenu ui_charactesPanel, UI_SessionResultsMenu ui_sessionResultsMenu, UI_BestiaryMenu ui_bestiaryMenu)
    {
        ui_profiliesButton.onClick.RemoveAllListeners();
        ui_profiliesButton.onClick.AddListener(() => ui_profiliesMenu.Show());

        _gameInstance.onUpdateProfile += UpdateNameProfile;
        UpdateNameProfile();
        
        ui_playButton.onClick.RemoveAllListeners();
        ui_playButton.onClick.AddListener(() => ui_charactesPanel.Show());

        // ui_settingsButton.onClick.RemoveAllListeners();
        // ui_settingsButton.onClick.AddListener(() => Application.Quit());

        ui_bestiaryButton.onClick.RemoveAllListeners();
        ui_bestiaryButton.onClick.AddListener(() => ui_bestiaryMenu.Show());
        
        ui_sessionResultsButton.onClick.RemoveAllListeners();
        ui_sessionResultsButton.onClick.AddListener(() => ui_sessionResultsMenu.Show());
        ui_sessionResultsButton.interactable = DataBase.current.SessionResults.HasRecords();

        ui_quitButton.onClick.RemoveAllListeners();
        ui_quitButton.onClick.AddListener(() => QuitGame());
    }

    private void QuitGame()
    {
        SaveLoadSystem.current.SaveGame(
            new SaveData(
                DataBase.current.Profilies.GetProfiles(), 
                _gameInstance.GetProfile()
            )
        );        
        Application.Quit();
    }

    private void UpdateNameProfile()
    {
        ui_nameProfileText.text = _gameInstance.GetProfile().name;
    }
}
