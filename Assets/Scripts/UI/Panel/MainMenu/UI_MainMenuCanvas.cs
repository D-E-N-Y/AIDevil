using UnityEngine;

public class UI_MainMenuCanvas : MonoBehaviour
{
    [SerializeField] private UI_MainMenu ui_MainMenu;
    [SerializeField] private UI_CharactersMenu ui_CharactersMenu;

    public void Initialize()
    {
        ui_CharactersMenu.Initialize();
        ui_CharactersMenu.Hide();

        ui_MainMenu.Initialize(ui_CharactersMenu);
        ui_MainMenu.Show();
    }
}
