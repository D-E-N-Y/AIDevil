using System;
using System.Collections.Generic;
using UnityEngine;

public class TradeZone : MonoBehaviour 
{
    public event Action OnCompleteTrade;
    
    [SerializeField] private Trader _trader;
    [SerializeField] private List<ItemStand> _itemStands;
    [SerializeField] private OfferStand _finishTrade;

    public void Initialize(GameInstance gameInstance, UI_Trade ui_trade, UI_Offer ui_offer)
    {
        _trader.Initilaize(gameInstance);
        _itemStands.ForEach(stand => stand.Initialize(ui_trade));
        
        _finishTrade.Initialize(ui_offer);
        _finishTrade.onYes += CompleteTrade;

        Despawn();
    }

    public void Spawn()
    {
        GenerateTradeItems();

        gameObject.SetActive(true);
    }

    public void Despawn()
    {
        gameObject.SetActive(false);
    }

    private void CompleteTrade()
    {
        Despawn();
        
        OnCompleteTrade?.Invoke();
    }

    private void GenerateTradeItems(int itemCount = 3)
    {
        _trader.GenerateItems(itemCount);
        List<Item> items = _trader.Items;

        for (int i = 0; i < _itemStands.Count; i++)
        {
            if (i < items.Count)
            {
                _itemStands[i].SetItem(items[i]);
            }
        }
    }
}