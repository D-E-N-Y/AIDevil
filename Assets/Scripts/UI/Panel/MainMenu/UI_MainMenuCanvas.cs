using UnityEngine;

public class UI_MainMenuCanvas : MonoBehaviour
{
    [SerializeField] private UI_MainMenu ui_mainMenu;
    [SerializeField] private UI_ProfiliesMenu ui_profiliesMenu;
    [SerializeField] private UI_CharactersMenu ui_charactersMenu;
    [SerializeField] private UI_SessionResultsMenu ui_sessionResultsMenu;

    private GameInstance _gameInstance;

    public void Initialize(GameInstance gameInstance)
    {
        _gameInstance = gameInstance;
        _gameInstance.onUpdateProfile += UpdateData;
        
        ui_sessionResultsMenu.Initialize(_gameInstance);
        ui_sessionResultsMenu.Hide();
        
        ui_charactersMenu.Initialize(_gameInstance);
        ui_charactersMenu.Hide();

        ui_profiliesMenu.Initialize(_gameInstance);
        ui_profiliesMenu.Hide();

        ui_mainMenu.Initialize(_gameInstance, ui_profiliesMenu, ui_charactersMenu, ui_sessionResultsMenu);
        ui_mainMenu.Show();
    }

    public void UpdateData()
    {
        ui_charactersMenu.UpdateData();
        ui_sessionResultsMenu.UpdateData();
        ui_mainMenu.UpdateData(ui_profiliesMenu, ui_charactersMenu, ui_sessionResultsMenu);
    }

    public void ShowProfiliesMenu() => ui_profiliesMenu.Show();
}
