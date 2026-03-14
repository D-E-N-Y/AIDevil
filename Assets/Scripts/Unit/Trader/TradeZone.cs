using System;
using System.Collections.Generic;
using UnityEngine;

public class TradeZone : MonoBehaviour 
{
    public event Action OnCompleteTrade;
    public event Action<HintType, Vector3, Action<Action>, Action<Action>> onStartHint;
    
    [SerializeField] private Trader _trader;
    [SerializeField] private List<ItemStand> _itemStands;
    [SerializeField] private OfferStand _finishTrade;

    public void Initialize(GameInstance gameInstance, UI_Trade ui_trade, UI_Offer ui_offer, UI_HintController ui_hintController)
    {
        _trader.Initilaize(gameInstance);
        _itemStands.ForEach(stand => stand.Initialize(ui_trade));
        
        _finishTrade.Initialize(ui_offer);
        _finishTrade.onYes += CompleteTrade;

        onStartHint += ui_hintController.ShowHint;

        Despawn();
    }

    public void Spawn()
    {
        GenerateTradeItems();

        gameObject.SetActive(true);

        onStartHint?.Invoke(
            HintType.Trader, 
            _trader.transform.position, 
            h => OnCompleteTrade += h,
            h => OnCompleteTrade -= h
        );
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