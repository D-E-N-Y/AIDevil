using System;
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

    public SessionSystem(PlayerCharacter playerCharacter, UI_SessionResultsGame ui_sessionResultsGame, UI_Pause ui_pause, WaveSystem waveSystem, TradeZone tradeZone)
    {
        _sesionResult = new SSesionResult();

        _sesionResult.namePlayerCharacter = playerCharacter.GetName();
        _sesionResult.name = $"{playerCharacter.GetName()} - {DateTime.Now}";

        playerCharacter.GetHealth().OnDead += LoseFinish;
        ui_pause.onExitSession += LoseFinish;

        _wallet = playerCharacter.GetWallet();

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

        CompleteSession(ESessionResult.LOSE);
    }

    private void SetResults(SCompleteWaveInfo waveInfo)
    {
        _sesionResult.collectCoins = waveInfo.collectCoins;
        _sesionResult.defeatEnemies = waveInfo.defeatEnemies;
        _sesionResult.completedWaves = waveInfo.completedWaves;
    }

    public void CompleteSession(ESessionResult result)
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