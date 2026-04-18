using System;
using System.Collections.Generic;
using UnityEngine;

public class SessionSystem
{
    private UI_SessionResultsGame _ui_sessionResultsGame;
    private WaveSystem _waveSystem;
    private TradeZone _tradeZone;
    
    private SSesionResult _sesionResult;
    
    private float _startTimeSession;
    private float _endTimeSession;

    // temporary field
    private Wallet _wallet;

    GameInstance _gameInstance;

    private ESessionResult _result;

    public SessionSystem(GameInstance gameInstance, PlayerCharacter playerCharacter, UI_SessionResultsGame ui_sessionResultsGame, UI_Pause ui_pause, WaveSystem waveSystem, TradeZone tradeZone)
    {
        _gameInstance = gameInstance;
        
        _sesionResult = new SSesionResult();

        _sesionResult.namePlayerCharacter = playerCharacter.Name;
        _sesionResult.name = $"{playerCharacter.Name} - {DateTime.Now}";

        playerCharacter.Health.OnDead += LoseFinish;
        ui_pause.onExitSession += LoseFinish;

        _wallet = playerCharacter.Wallet;

        _ui_sessionResultsGame = ui_sessionResultsGame;

        _waveSystem = waveSystem;

        // _waveSystem.finishWaves += CompleteSession;

        _waveSystem.OnCompleteWave += CompleteWave;
        _waveSystem.sendResults += SetResults;

        _tradeZone = tradeZone;
        _tradeZone.OnCompleteTrade += CompleteTrade;
    }

    public void StartSession()
    {
        _startTimeSession = Time.unscaledTime;

        _waveSystem.StartWave();
    }

    private void CompleteWave()
    {
        _tradeZone.Spawn();
    }

    private void CompleteTrade()
    {
        _waveSystem.StartWave();
    }

    public void LoseFinish()
    {
        _waveSystem.StopWave();
        _waveSystem.SendWaveResults();

        if (_waveSystem.IsInfinityWaves)
        {
            WinFinish();
        }
        else
        {
            CompleteSession(ESessionResult.LOSE);
        }
    }

    public void WinFinish()
    {
        string levelID = _gameInstance.GameLevelsManager.CurrentGameLevel.ID;
        _gameInstance.ProfileManager.CurrentProfile.GameLevelsProgress.AddGameLevel(levelID);
        
        CompleteSession(ESessionResult.WIN);
    }

    private void SetResults(SCompleteWaveInfo waveInfo)
    {
        _sesionResult.defeatEnemies = waveInfo.defeatEnemies;
        _sesionResult.completedWaves = waveInfo.completedWaves;
    }

    private void CompleteSession(ESessionResult result)
    {
        _endTimeSession = Time.unscaledTime;

        _sesionResult.result = result;
        _sesionResult.time = new STime((int)(_endTimeSession - _startTimeSession));
        // _sesionResult.collectCoins = _wallet.AllCollectedMoney;
        
        _sesionResult.collectResources = GetColletResources();

        _gameInstance.ProfileManager.CurrentProfile.SessionResultsProgress.AddSessionResult(_sesionResult);
        _gameInstance.ProfileManager.CurrentProfile.Wallet.AddResources(GetColletResources());

        _ui_sessionResultsGame.SetResult(_sesionResult);
        _ui_sessionResultsGame.Show();
    }

    private Dictionary<ResourceType, int> GetColletResources()
    {
        Dictionary<ResourceType, int> resources = new Dictionary<ResourceType, int>();

        foreach (ResourceType resource in Enum.GetValues(typeof(ResourceType)))
        {
            if (resource == ResourceType.Credits) continue;

            if (_wallet.HasResources(resource))
            {
                resources[resource] = _wallet.Resources[resource];
            }
        }

        return resources;
    }
}