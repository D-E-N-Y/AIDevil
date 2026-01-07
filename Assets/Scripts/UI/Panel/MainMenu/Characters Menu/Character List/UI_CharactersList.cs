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

    private GameInstance _gameInstance;

    public void Initialize(GameInstance gameInstance, UI_CharactersMenu ui_charactersMenu)
    {
        _gameInstance = gameInstance;
        _ui_charactersMenu = ui_charactersMenu;

        UpdateData();
    }

    public void UpdateData()
    {
        selected_ui_Character = null;
        List<Player> characters = DataBase.current.Characters.GetCharacters();
        
        List<UI_Character> ui_characters = new List<UI_Character>();
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

    
}