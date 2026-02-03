using System;
using UnityEngine;

public class SessionSystem : MonoBehaviour 
{
    private UI_SessionResultsGame _ui_sessionResultsGame;
    private WaveSystem _waveSystem;
    
    private SSesionResult _sesionResult;
    
    private float _startTimeSession;
    private float _endTimeSession;

    // temporary field
    private Wallet _wallet;

    public void Initialize(PlayerCharacter playerCharacter, UI_SessionResultsGame ui_sessionResultsGame, WaveSystem waveSystem)
    {
        _sesionResult = new SSesionResult();

        _sesionResult.namePlayerCharacter = playerCharacter.GetName();
        _sesionResult.name = $"{playerCharacter.GetName()} - {DateTime.Now}";

        playerCharacter.GetHealth().OnDead += DeathPlayerCharacter;
        _wallet = playerCharacter.GetWallet();

        _ui_sessionResultsGame = ui_sessionResultsGame;

        _waveSystem = waveSystem;
        _waveSystem.finishWaves += CompleteSession;
        _waveSystem.sendResults += SetResults;
    }

    public void StartSession()
    {
        _startTimeSession = Time.unscaledTime;

        _waveSystem.StartWave();
    }

    private void DeathPlayerCharacter()
    {
        _waveSystem.StopWave();
        _waveSystem.SendWaveResults();

        CompleteSession(ESessionResult.LOSE);
    }

    private void SetResults(SCompleteWaveInfo waveInfo)
    {
        _sesionResult.collectCoins = waveInfo.collectCoins;
        _sesionResult.defeatEnemies = waveInfo.defeatEnemies;
        _sesionResult.completedWaves = waveInfo.completedWaves;
    }

    private void CompleteSession(ESessionResult result)
    {
        _endTimeSession = Time.unscaledTime;

        _sesionResult.result = result;
        _sesionResult.time = new STime((int)(_endTimeSession - _startTimeSession));
        _sesionResult.collectCoins = _wallet.AllCollectedMoney;

        GameInstance.current.AddSessionResult(_sesionResult);

        _ui_sessionResultsGame.SetResult(_sesionResult);
        _ui_sessionResultsGame.Show();
    }
}