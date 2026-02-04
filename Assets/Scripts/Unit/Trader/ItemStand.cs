using System;
using UnityEngine;

public class ItemStand : MonoBehaviour 
{
    public event Action OnItemChanged;
    
    [SerializeField] private WorldItem _worldItem;
    
    private Item _item;
    public Item Item => _item;

    public void Initialize()
    {
        _worldItem.gameObject.SetActive(false);
    }

    public void SetItem(Item item)
    {
        _item = item;
        _worldItem.Initialize(item, 1, false);

        OnItemChanged?.Invoke();
    }
}