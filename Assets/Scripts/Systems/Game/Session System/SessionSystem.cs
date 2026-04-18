using System;
using System.Collections.Generic;
using UnityEngine;

public class SessionSystem : MonoBehaviour
{
    [SerializeField] private WaveSystem _waveSystem;
    [SerializeField] private LandSystem _landSystem;
    [SerializeField] private ResourceSystem _resourceSystem;
    [SerializeField] private WorldPickupSystem _worldPickupSystem;

    [SerializeField] private TradeZone _tradeZone;
    [SerializeField] private EndGame _endGame;
    
    private PlayerCharacter _playerCharacter;

    private float _startTimeSession;
    private float _endTimeSession;

    private FinishSession _finishSession;

    public void Initialize(GameInstance gameInstance, GameUICanvas gameUICanvas, PlayerCharacter playerCharacter)
    {
        _playerCharacter = playerCharacter;
        _playerCharacter.Health.OnDead += DeathCharacter;
        gameUICanvas.UIPause.onExitSession += ExitSession;

        _worldPickupSystem.Initialize();
        _landSystem.Initialize();

        _waveSystem.Initialize(gameInstance.GameLevelsManager.CurrentGameLevel.WaveConfig.Waves, _worldPickupSystem, playerCharacter);
        _waveSystem.OnCompleteWave += CompleteWave;
        _waveSystem.OnFinishWaves += FinishWaves;
        gameUICanvas.UIGameplay.UIWave.Initialize(_waveSystem);

        _resourceSystem.Initialize(gameInstance.GameLevelsManager.CurrentGameLevel.Resources, _landSystem, _worldPickupSystem, gameUICanvas.UIGameplay.UIHintController);

        _tradeZone.Initialize(gameInstance, gameUICanvas.UIGameplay.UITrade, gameUICanvas.UIGameplay.UIOffer, gameUICanvas.UIGameplay.UIHintController);
        _tradeZone.OnCompleteTrade += CompleteTrade;

        _endGame.Initialize(gameUICanvas.UIGameplay.UIOffer);
        _endGame.OnFinishSession += FinishSession;
        _endGame.OnStartInfinityWaves += StartInfinityWaves;

        _finishSession = new FinishSession(gameInstance, gameUICanvas.UIResultsSession);
    }

    public void StartSession()
    {
        Time.timeScale = 1f;
        _startTimeSession = Time.unscaledTime;

        _waveSystem.StartWave();
        _resourceSystem.SpawnResoure.StartSpawn();
    }

    private void CompleteWave()
    {
        _tradeZone.Spawn();
        _resourceSystem.SpawnResoure.StopSpawn();
    }

    private void FinishWaves()
    {
        _finishSession.Win();
        _endGame.Spawn();
    }

    private void CompleteTrade()
    {
        _waveSystem.StartWave();
        _resourceSystem.SpawnResoure.StartSpawn();
    }

    private void StartInfinityWaves()
    {
        _waveSystem.StartInfinityWaves();
    }

    private void DeathCharacter()
    {
        if (!_waveSystem.IsInfinityWaves)
        {
            _finishSession.Lose();
        }
        
        FinishSession();
    }

    private void ExitSession()
    {
        if (!_waveSystem.IsInfinityWaves)
        {
            _finishSession.Lose();
        }
        
        FinishSession();
    }

    private void FinishSession()
    {
        SetResults();
        _finishSession.Finish();
    }

    private void SetResults()
    {
        _endTimeSession = Time.unscaledTime;
        SCompleteWaveInfo waveResult = _waveSystem.GetWaveResult();
        
        SSesionResult result = new SSesionResult();
        result.namePlayerCharacter = _playerCharacter.Name;
        result.name = $"{_playerCharacter.Name} - {DateTime.Now}";
        result.time = new STime((int)(_endTimeSession - _startTimeSession));
        result.defeatEnemies = waveResult.defeatEnemies;
        result.completedWaves = waveResult.completedWaves;
        result.collectResources = GetColletResources();

        _finishSession.SetResult(result);
    }

    private Dictionary<ResourceType, int> GetColletResources()
    {
        Dictionary<ResourceType, int> resources = new Dictionary<ResourceType, int>();

        foreach (ResourceType resource in Enum.GetValues(typeof(ResourceType)))
        {
            if (resource == ResourceType.Credits) continue;

            if (_playerCharacter.Wallet.HasResources(resource))
            {
                resources[resource] = _playerCharacter.Wallet.Resources[resource];
            }
        }

        return resources;
    }
}