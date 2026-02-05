using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Trade : UI_Panel 
{
    [SerializeField] private Image ui_itemIconImage;

    [SerializeField] private TextMeshProUGUI ui_itemNameText;
    [SerializeField] private TextMeshProUGUI ui_itemRareText;
    [SerializeField] private TextMeshProUGUI ui_itemPriceText;
    
    [SerializeField] private Button ui_tradeButton;
    [SerializeField] private Button ui_closePanelButton;

    private WorldItem _worldItem;
    private ItemContext _itemContext;

    public void Initialize(WorldItem worldItem, ItemContext itemContext)
    {
        _worldItem = worldItem;
        _itemContext = itemContext;
    }

    private void UpdatePanel()
    {
        ui_itemIconImage.sprite = _worldItem.Item.Icon;
        ui_itemNameText.text = _worldItem.Item.Name;
        ui_itemRareText.text = _worldItem.Item.Rare.ToString();
        ui_itemPriceText.text = _worldItem.Item.Price.ToString();
    }

    private void Trade()
    {
        _worldItem.AllowPickUp();
        _worldItem.PickUp(_itemContext);

        Hide();
    }
}