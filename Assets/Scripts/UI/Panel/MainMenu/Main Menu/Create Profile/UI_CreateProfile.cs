using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_CreateProfile : UI_Panel 
{
    [SerializeField] private Button ui_createButton;
    [SerializeField] private Button ui_cancelButton;
    [SerializeField] private TMP_InputField ui_nameInputField;

    private GameInstance _gameInstance;

    public void Initialize(GameInstance gameInstance)
    {
        _gameInstance = gameInstance;

        ui_createButton.onClick.RemoveAllListeners();
        ui_createButton.onClick.AddListener(() => CreateProfile());

        ui_cancelButton.onClick.RemoveAllListeners();
        ui_cancelButton.onClick.AddListener(() => Cancel());

        ui_nameInputField.onValueChanged.RemoveAllListeners();
        ui_nameInputField.onValueChanged.AddListener((string name) => OnValidName(name));

        OnValidName("");
    }

    private void OnValidName(string name)
    {
        if(_gameInstance.DBProfilies().HasProfilieByName(name))
        {
            ui_createButton.interactable = false;
        }
        else
        {
            if(string.IsNullOrEmpty(name))
            {
                ui_createButton.interactable = false;
            } 
            else
            {
                ui_createButton.interactable = true;
            }
        }
    }

    private void CreateProfile()
    {
        Profile newProfile = new Profile(
            ui_nameInputField.text,
            null,
            _gameInstance.GetCopyDBCharacters(),
            new DB_SessionResults()
        );
        
        _gameInstance.DBProfilies().AddProfile(newProfile);

        ui_nameInputField.text = string.Empty;
        OnValidName("");

        Hide();
    }

    private void Cancel()
    {
        ui_nameInputField.text = string.Empty;
        OnValidName("");

        Hide();
    }
}