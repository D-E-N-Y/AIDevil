using UnityEngine;
using UnityEngine.UI;

public class WorldItem : WorldPickup 
{
    [SerializeField] private Item _item;
    public Item Item => _item;

    private int _amount;
    public int Amount => _amount;

    [SerializeField] private Image _iconImage;

    public override PickupType Type => PickupType.Item;

    public void Initialize(Item item, int amount = 1, bool canPickUp = true)
    {
        if(item == null) return;
        
        _item = item;
        _amount = amount;
        _canPickUp = canPickUp;

        _iconImage.sprite = item.Icon;

        _isPickedUp = false;
        gameObject.SetActive(true);
    }

    public override void PickUp(UnitContext context)
    {
        if (!_canPickUp) return;
        
        context.Inventory.AddItem(_item);

        _isPickedUp = true;
        gameObject.SetActive(false);
    }
}