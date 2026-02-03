using UnityEngine;
using UnityEngine.UI;

public class WorldItem : WorldPickup 
{
    [SerializeField] private Item _item;
    public Item Item => _item;

    private int _amount;
    public int Amount => _amount;

    [SerializeField] private Image _iconImage;

    private void Awake() 
    {
        Initialize(_item);
    }

    public void Initialize(Item item, int amount = 1)
    {
        _item = item;
        _amount = amount;

        _iconImage.sprite = item.Icon;

        gameObject.SetActive(true);
    }

    public override void PickUp(ItemContext context)
    {
        context.Inventory.AddItem(_item);

        gameObject.SetActive(false);
    }
}