using UnityEngine;
using UnityEngine.UI;

public class WorldItem : MonoBehaviour 
{
    [SerializeField] private Item _item;
    public Item Item => _item;

    [SerializeField] private Image _iconImage;

    private void Awake() 
    {
        Initialize(_item);
    }

    public void Initialize(Item item)
    {
        _item = item;
        _iconImage.sprite = item.Icon;

        gameObject.SetActive(true);
    }

    public void PickUp(Inventory inventory)
    {
        inventory.AddItem(_item);
        gameObject.SetActive(false);
    }
}