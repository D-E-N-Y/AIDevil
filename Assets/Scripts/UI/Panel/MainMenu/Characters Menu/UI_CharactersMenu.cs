using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_CharactersMenu : UI_Panel
{
    [SerializeField] private UI_CharactersList ui_charactersList;
    [SerializeField] private UI_CharacterDescription ui_characterDescription;

    [SerializeField] private Button ui_closeButton;
    [SerializeField] private Button ui_chooseGameLevelButton;

    private GameInstance _gameInstance;

    public void Initialize(GameInstance gameInstance, UI_GameLevelsMenu ui_gameLevelsMenu)
    {
        _gameInstance = gameInstance;
        
        ui_characterDescription.Initialize();
        ui_charactersList.Initialize(gameInstance, this);

        ui_closeButton.onClick.RemoveAllListeners();
        ui_closeButton.onClick.AddListener(() => Hide()); 
        
        ui_chooseGameLevelButton.onClick.RemoveAllListeners();
        ui_chooseGameLevelButton.onClick.AddListener(() => ui_gameLevelsMenu.Show());
    }


    public void Select(PlayerCharacter character)
    {
        ui_characterDescription.SetCharacterInfo(character);
        _gameInstance.SetPlayer(character);
    }
}
