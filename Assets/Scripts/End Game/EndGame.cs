using UnityEngine;

public class EndGame : MonoBehaviour 
{
    [SerializeField] private OfferStand _finishSession;
    [SerializeField] private OfferStand _infinityWaves;

    public void Initialize(WaveSystem waveSystem, SessionSystem sessionSystem, UI_Offer ui_offer)
    {
        _finishSession.Initialize(ui_offer);
        _infinityWaves.Initialize(ui_offer);

        _finishSession.onYes += () => 
        {
            sessionSystem.WinFinish();
            Despawn();
        };

        _infinityWaves.onYes += () => 
        {
            waveSystem.StartInfinityWaves();
            Despawn();
        };

        waveSystem.finishWaves += Spawn;

        Despawn();
    }

    public void Spawn()
    {
        gameObject.SetActive(true);
    }

    public void Despawn()
    {
        gameObject.SetActive(false);
    }
}