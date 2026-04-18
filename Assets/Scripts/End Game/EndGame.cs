using System;
using UnityEngine;

public class EndGame : MonoBehaviour 
{
    [SerializeField] private OfferStand _finishSession;
    [SerializeField] private OfferStand _infinityWaves;

    public event Action OnFinishSession;
    public event Action OnStartInfinityWaves;

    public void Initialize(UI_Offer ui_offer)
    {
        _finishSession.Initialize(ui_offer);
        _infinityWaves.Initialize(ui_offer);

        _finishSession.onYes += () => 
        {
            OnFinishSession?.Invoke();
            Despawn();
        };

        _infinityWaves.onYes += () => 
        {
            OnStartInfinityWaves?.Invoke();
            Despawn();
        };

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