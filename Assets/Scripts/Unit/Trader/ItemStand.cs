using System;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class ItemStand : MonoBehaviour 
{
    public event Action OnItemChanged;
    
    [SerializeField] private WorldItem _worldItem;
    
    private UI_Trade _ui_trade;

    private SphereCollider _sphereCollider;

    private Item _item;
    public Item Item => _item;

    private bool _isTradeItem;
    public bool IsTradeItem => _isTradeItem;

    private ItemContext _itemContext;

    public void Initialize(UI_Trade ui_trade)
    {
        _ui_trade = ui_trade;
        _ui_trade.Initialize();

        _worldItem.gameObject.SetActive(false);

        _sphereCollider = GetComponent<SphereCollider>();
        _sphereCollider.isTrigger = true;
        _sphereCollider.radius = 0.5f;

        gameObject.layer = LayerMask.NameToLayer("ItemStand");
    }

    public void SetItem(Item item)
    {
        _item = item;
        _worldItem.Initialize(item, 1, false);

        _isTradeItem = false;

        OnItemChanged?.Invoke();
    }

    private void Trade()
    {
        if(_itemContext == null)
        {
            Debug.Log("_itemContext null");
            return;
        }
        
        _itemContext.Wallet.RemoveMoney(_item.Price);

        _worldItem.AllowPickUp();
        _worldItem.PickUp(_itemContext);

        _isTradeItem = true;
        ClearSubscriptions();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(_isTradeItem) return;

        _itemContext = other.gameObject.GetComponent<PlayerCharacter>().GetItemContext();

        _ui_trade.SetItem(_item, _itemContext.Wallet.HasEnoughMoney(_item.Price));
        _ui_trade.Show();

        AddSubcriptions();
    }

    private void OnTriggerExit(Collider other)
    {
        if(_isTradeItem) return;

        _itemContext = null;

        _ui_trade.Hide();

        ClearSubscriptions();
    }

    private void AddSubcriptions()
    {
        _ui_trade.onTrade += Trade;
    }

    private void ClearSubscriptions()
    {
        _ui_trade.onTrade -= Trade;
    }

    private void OnDestroy()
    {
        ClearSubscriptions();
    }
}