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

    private UI_ProfiliesMenu _ui_profiliesMenu;
    private UI_CharactersMenu _ui_charactersMenu;
    private UI_SessionResultsMenu _ui_sessionResultsMenu;
    private UI_BestiaryMenu _ui_bestiaryMenu;

    private GameInstance _gameInstance;

    public void Initialize(GameInstance gameInstance, UI_ProfiliesMenu ui_profiliesMenu, UI_CharactersMenu ui_charactesPanel, UI_SessionResultsMenu ui_sessionResultsMenu, UI_BestiaryMenu ui_bestiaryMenu)
    {
        _gameInstance = gameInstance;

        _ui_profiliesMenu = ui_profiliesMenu;
        _ui_charactersMenu = ui_charactesPanel;
        _ui_sessionResultsMenu = ui_sessionResultsMenu;
        _ui_bestiaryMenu = ui_bestiaryMenu;

        AddSubscriptions();

        SetButtonAction();
    }

    private void SetButtonAction()
    {
        ui_profiliesButton.onClick.RemoveAllListeners();
        ui_profiliesButton.onClick.AddListener(() => _ui_profiliesMenu.Show());

        UpdateNameProfile();
        
        ui_playButton.onClick.RemoveAllListeners();
        ui_playButton.onClick.AddListener(() => _ui_charactersMenu.Show());

        // ui_settingsButton.onClick.RemoveAllListeners();
        // ui_settingsButton.onClick.AddListener(() => Application.Quit());

        ui_bestiaryButton.onClick.RemoveAllListeners();
        ui_bestiaryButton.onClick.AddListener(() => _ui_bestiaryMenu.Show());
        
        ui_sessionResultsButton.onClick.RemoveAllListeners();
        ui_sessionResultsButton.onClick.AddListener(() => _ui_sessionResultsMenu.Show());
        ui_sessionResultsButton.interactable = _gameInstance.ProfileManager.CurrentProfile.SessionResultsProgress.HasSessionResults();

        ui_quitButton.onClick.RemoveAllListeners();
        ui_quitButton.onClick.AddListener(() => QuitGame());
    }

    private void UpdateData()
    {
        UpdateNameProfile();
        
        ui_sessionResultsButton.interactable = _gameInstance.ProfileManager.CurrentProfile.SessionResultsProgress.HasSessionResults();
    }

    private void QuitGame()
    {
        _gameInstance.SaveLoadSystem.SaveData();      
        Application.Quit();
    }

    private void UpdateNameProfile()
    {
        ui_nameProfileText.text = _gameInstance.ProfileManager.CurrentProfile.Name;
    }

    protected override void AddSubscriptions()
    {
        base.AddSubscriptions();
        _gameInstance.ProfileManager.onCurrentProfileChanged += UpdateData;
    }

    protected override void ClearSubscriptions()
    {
        base.ClearSubscriptions();
        _gameInstance.ProfileManager.onCurrentProfileChanged -= UpdateData;
    }
}
