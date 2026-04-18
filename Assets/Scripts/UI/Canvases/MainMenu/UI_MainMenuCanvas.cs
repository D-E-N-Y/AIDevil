using UnityEngine;

public class UI_MainMenuCanvas : MonoBehaviour
{
    [SerializeField] private UI_MainMenu ui_mainMenu;
    [SerializeField] private UI_ProfiliesMenu ui_profiliesMenu;
    [SerializeField] private UI_CharactersMenu ui_charactersMenu;
    [SerializeField] private UI_CharacterUpgradeMenu ui_characterUpgradeMenu;
    [SerializeField] private UI_GameLevelsMenu ui_gameLevelsMenu;
    [SerializeField] private UI_BestiaryMenu ui_bestiaryMenu;  
    [SerializeField] private UI_SessionResultsMenu ui_sessionResultsMenu;

    private GameInstance _gameInstance;

    public void Initialize(GameInstance gameInstance)
    {
        _gameInstance = gameInstance;
        
        ui_sessionResultsMenu.Initialize(_gameInstance);
        ui_sessionResultsMenu.Hide();
        
        ui_bestiaryMenu.Initialize(_gameInstance);
        ui_bestiaryMenu.Hide();

        ui_characterUpgradeMenu.Initialize(_gameInstance);
        ui_characterUpgradeMenu.Hide();

        ui_gameLevelsMenu.Initialize(_gameInstance);
        ui_gameLevelsMenu.Hide();

        ui_charactersMenu.Initialize(_gameInstance, ui_gameLevelsMenu, ui_characterUpgradeMenu);
        ui_charactersMenu.Hide();

        ui_profiliesMenu.Initialize(_gameInstance);
        ui_profiliesMenu.Hide();

        ui_mainMenu.Initialize(_gameInstance, ui_profiliesMenu, ui_charactersMenu, ui_sessionResultsMenu, ui_bestiaryMenu);
        ui_mainMenu.Show();
    }

    public void ShowProfiliesMenu() => ui_profiliesMenu.Show();
}
