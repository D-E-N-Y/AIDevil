using System;
using UnityEngine;

public class OfferStand : MonoBehaviour 
{
    public event Action onYes;
    public event Action onNo;
    
    private UI_Offer _ui_offer;
    [SerializeField] private string _message;

    public void Initialize(UI_Offer ui_offer)
    {
        _ui_offer = ui_offer;
    }

    void OnTriggerEnter(Collider other)
    {
        _ui_offer.SetMessageText(_message);
        _ui_offer.SetActions(onYes, onNo);
        
        _ui_offer.Show();
    }

    void OnTriggerExit(Collider other)
    {
        _ui_offer.Hide();
    }
}