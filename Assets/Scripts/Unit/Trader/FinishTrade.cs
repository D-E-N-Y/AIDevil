using System;
using UnityEngine;

public class FinishTrade : MonoBehaviour 
{
    public event Action OnFinishTrade;
    
    void OnTriggerEnter(Collider other)
    {
        OnFinishTrade?.Invoke();
    }
}