using System;
using UnityEngine;

public class FinishTrade : MonoBehaviour 
{
    public event Action OnFinishTrade;
    
    private UI_FinishTrade _ui_finishTrade;

    public void Initialize(UI_FinishTrade ui_finishTrade)
    {
        _ui_finishTrade = ui_finishTrade;
        _ui_finishTrade.Initialize(this);
    }

    void OnTriggerEnter(Collider other)
    {
        _ui_finishTrade.Show();
    }

    void OnTriggerExit(Collider other)
    {
        _ui_finishTrade.Hide();
    }

    public void Finish()
    {
        OnFinishTrade?.Invoke();
    }
}