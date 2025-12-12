using UnityEngine;

public class UI_MainMenuCanvas : MonoBehaviour
{
    [SerializeField] private UI_MainMenu ui_mainMenu;
    [SerializeField] private UI_CharactersMenu ui_charactersMenu;
    [SerializeField] private UI_SessionResultsMenu ui_sessionResultsMenu;

    public void Initialize()
    {
        ui_sessionResultsMenu.Initialize(GameInstance.current);
        ui_sessionResultsMenu.Hide();
        
        ui_charactersMenu.Initialize(GameInstance.current);
        ui_charactersMenu.Hide();

        ui_mainMenu.Initialize(ui_charactersMenu, ui_sessionResultsMenu);
        ui_mainMenu.Show();
    }
}
