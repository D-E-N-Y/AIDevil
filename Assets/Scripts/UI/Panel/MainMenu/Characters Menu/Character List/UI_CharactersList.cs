using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UI_CharactersList : UI_Panel 
{
    [SerializeField] private Transform container;
    [SerializeField] private UI_Character ui_characterPrefab;

    private UI_Character selected_ui_Character;
    private UI_CharactersMenu _ui_charactersMenu;

    private List<UI_Character> ui_characters;

    private UI_MainMenuCanvas _mainMenuCanvas;

    private GameInstance _gameInstance;

    public void Initialize(GameInstance gameInstance, UI_CharactersMenu ui_charactersMenu)
    {
        _gameInstance = gameInstance;
        
        _ui_charactersMenu = ui_charactersMenu;

        AddSubscriptions();

        CreateElements();
        UpdateData();
    }

    private void CreateElements()
    {
        List<Player> characters = _gameInstance.GetDataBase().Characters.GetCharacters();
        
        ui_characters = new List<UI_Character>();
        ui_characters = container.GetComponentsInChildren<UI_Character>(true).ToList();

        int residue = Math.Abs(ui_characters.Count - characters.Count);
        if(residue > 0)
        {
            for(int i = 0; i < residue; i++)
            {
                UI_Character _ui_character = Instantiate(ui_characterPrefab, container);
                ui_characters.Add(_ui_character);
            }
        }

        for(int i = 0; i < characters.Count; i++)
        {
            ui_characters[i].Initialize(characters[i], this);
            ui_characters[i].Show();
        }
    }

    public void UpdateData()
    {
        if(selected_ui_Character)
        {
            selected_ui_Character.UnSelect();
        }
        selected_ui_Character = null;

        if(_gameInstance.GetPlayerCharacter() != null)
        {
            Player _playerCharacter = _gameInstance.GetPlayerCharacter();
            UI_Character ui_character = ui_characters.Find(x => x.GetCharacter() == _playerCharacter);
            ui_character.Select();
        }
        else
        {
            ui_characters[0].Select();
        }
    }

    public void Select(UI_Character ui_character)
    {
        if(ui_character != null)
        {
            if(ui_character == selected_ui_Character) return;

            if(selected_ui_Character != null)
            {
                selected_ui_Character.UnSelect();
                selected_ui_Character = null;
            }

            selected_ui_Character = ui_character;
            _ui_charactersMenu.Select(ui_character.GetCharacter());
        }
    }

    public UI_Character GetSelectedUICharacter() => selected_ui_Character;

    protected override void AddSubscriptions()
    {
        base.AddSubscriptions();
        _gameInstance.onCurrentProfileChanged += UpdateData;
    }

    protected override void ClearSubscriptions()
    {
        base.ClearSubscriptions();
        _gameInstance.onCurrentProfileChanged -= UpdateData;
    }
}