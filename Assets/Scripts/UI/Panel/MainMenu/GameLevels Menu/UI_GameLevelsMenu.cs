using System.Collections.Generic;
using Unity.Barracuda;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_GameLevelsMenu : UI_Panel 
{
    [SerializeField] private UI_GameLevelsList ui_gameLevelsList;
    [SerializeField] private UI_GameLevelDescription ui_gameLevelDescription;
    
    [SerializeField] private Button ui_closeButton;
    [SerializeField] private Button ui_fightButton;

    private GameInstance _gameInstance;

    public void Initialize(GameInstance gameInstance)
    {
        _gameInstance = gameInstance;

        ui_gameLevelDescription.Initialize();

        ui_gameLevelsList.onSelectGameLevel += SelectGameLevel;
        ui_gameLevelsList.Initialize(_gameInstance.DataBase.GameLevels.GameLevels);

        ui_closeButton.onClick.RemoveAllListeners();
        ui_closeButton.onClick.AddListener(() => Hide());
    }

    private void SelectGameLevel(string nameGameLevel)
    {
        _gameInstance.GameLevelsManager.SetCurrentGameLevel(nameGameLevel);
        ui_gameLevelDescription.UpdateInfo(_gameInstance.GameLevelsManager.CurrentGameLevel);

        UpdateFightButton(nameGameLevel);
    }

    private void UpdateFightButton(string nameGameLevel)
    {
        IReadOnlyList<GameLevel> requiredLevels = _gameInstance.DataBase.GameLevels.GetGameLevelByName(nameGameLevel).RequiredLevels;

        ui_fightButton.onClick.RemoveAllListeners();
        ui_fightButton.onClick.AddListener(() => SceneManager.LoadScene(nameGameLevel));
        ui_fightButton.interactable = IsLevelAvailable(requiredLevels);
    }

    private bool IsLevelAvailable(IReadOnlyList<GameLevel> requiredLevels)
    {
        if (requiredLevels.Count > 0)
        {
            foreach (GameLevel level in requiredLevels)
            {
                if (!_gameInstance.ProfileManager.CurrentProfile.GameLevelsProgress.IsGameLevelCompleted(level.ID))
                {
                    return false;
                }
            }
        }

        return true;
    }

    public override void Show()
    {
        base.Show();
        ui_gameLevelsList.UpdateData();
    }
}