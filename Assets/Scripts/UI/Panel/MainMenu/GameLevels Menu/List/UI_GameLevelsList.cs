using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UI_GameLevelsList : UI_Panel 
{
    public event Action<string> onSelectGameLevel;
    
    [SerializeField] private UI_GameLevel ui_gameLevelPrefab;
    private UI_GameLevel _selectGameLevel;

    [SerializeField] private Transform _containerUIGameLveles;

    private List<UI_GameLevel> ui_gameLevels;

    public void Initialize(IReadOnlyList<GameLevel> levels)
    {
        CreateUIGameLevels(levels);
    }

    private void CreateUIGameLevels(IReadOnlyList<GameLevel> levels)
    {
        ui_gameLevels = _containerUIGameLveles.GetComponentsInChildren<UI_GameLevel>(true).ToList();
        ui_gameLevels.ForEach(x => x.Hide());

        int remmant = levels.Count - ui_gameLevels.Count;
        if (remmant > 0)
        {
            for (int i = 0; i < remmant; i++)
            {
                UI_GameLevel ui_gameLevel = Instantiate(ui_gameLevelPrefab, _containerUIGameLveles);
                ui_gameLevel.Hide();

                ui_gameLevels.Add(ui_gameLevel);
            }
        }

        for (int i = 0; i < levels.Count; i++)
        {
            ui_gameLevels[i].Initialize(levels[i].Name);
            ui_gameLevels[i].Show();

            ui_gameLevels[i].onSelect -= SelectGameLevel;
            ui_gameLevels[i].onSelect += SelectGameLevel;
        }

        ui_gameLevels.First().Select();
    }

    private void SelectGameLevel(UI_GameLevel ui_gameLevel)
    {
        if (_selectGameLevel != null)
        {
            _selectGameLevel.UnSelect();
            _selectGameLevel = null;
        }

        _selectGameLevel = ui_gameLevel;

        onSelectGameLevel?.Invoke(_selectGameLevel.NameGameLevel);
    }
}