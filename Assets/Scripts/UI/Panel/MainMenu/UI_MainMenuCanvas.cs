using UnityEngine;

public class UI_MainMenuCanvas : MonoBehaviour
{
    [SerializeField] private UI_MainMenu ui_mainMenu;
    [SerializeField] private UI_CharactersMenu ui_charactersMenu;

    public void Initialize()
    {
        ui_charactersMenu.Initialize(GameInstance.current);
        ui_charactersMenu.Hide();

        ui_mainMenu.Initialize(ui_charactersMenu);
        ui_mainMenu.Show();
    }
}
