using UnityEngine;

public class SessionSystem : MonoBehaviour 
{
    private UI_ResultsSession _ui_resultsSession;
    private WaveSystem _waveSystem;
    
    private SSesionResult _sesionResult;
    
    private float _startTimeSession;
    private float _endTimeSession;

    public void Initialize(Player player, UI_ResultsSession ui_resultsSession, WaveSystem waveSystem)
    {
        _sesionResult = new SSesionResult();

        _sesionResult.playerCharacter = player;
        player.onDead += DeathPlayerCharacter;

        _ui_resultsSession = ui_resultsSession;

        _waveSystem = waveSystem;
        _waveSystem.finishWaves += CompleteSession;
        _waveSystem.sendResults += SetResults;
    }

    public void StartSession()
    {
        _startTimeSession = Time.unscaledTime;

        _waveSystem.StartWave();
    }

    private void DeathPlayerCharacter(IHealth health)
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

        _ui_resultsSession.SetResult(_sesionResult);
        _ui_resultsSession.Show();
    }
}