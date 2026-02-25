using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Trade : UI_Panel 
{
    public event Action onTrade;

    [SerializeField] private UI_Item ui_item;
    [SerializeField] private Button ui_tradeButton;

    public void Initialize()
    {
        ui_item.Initialize();
    }

    public void UpdatePanel(Item item, bool canBuy)
    {
        ui_item.SetItem(item);
        SetButtons(canBuy);
    }

    private void SetButtons(bool canBuy)
    {
        ui_tradeButton.interactable = canBuy;
        ui_tradeButton.onClick.RemoveAllListeners();
        ui_tradeButton.onClick.AddListener(() => Trade());
    }

    private void Trade()
    {
        onTrade?.Invoke();
        Hide();
    }
}