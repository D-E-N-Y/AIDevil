using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_CharactersMenu : UI_Panel
{
    [SerializeField] private UI_CharactersList ui_charactersList;
    [SerializeField] private UI_CharacterDescription ui_characterDescription;

    [SerializeField] private Button ui_closeButton;
    [SerializeField] private Button ui_fightButton;

    private GameInstance _gameInstance;

    public void Initialize(GameInstance gameInstance)
    {
        _gameInstance = gameInstance;
        
        ui_charactersList.Initialize(gameInstance, this);

        ui_closeButton.onClick.RemoveAllListeners();
        ui_closeButton.onClick.AddListener(() => Hide()); 

        ui_fightButton.onClick.RemoveAllListeners();
        ui_fightButton.onClick.AddListener(() => SceneManager.LoadScene("GameScene"));
    }


    public void Select(Player character)
    {
        ui_characterDescription.SetCharacterInfo(character);
        _gameInstance.SetPlayer(character);
    }
}
