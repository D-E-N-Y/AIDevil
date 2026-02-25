using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Item : UI_Panel 
{
    [SerializeField] private RarityColors _rarityColors;

    [SerializeField] private Image ui_itemIconImage;

    [SerializeField] private TextMeshProUGUI ui_itemNameText;
    [SerializeField] private TextMeshProUGUI ui_itemRareText;
    [SerializeField] private TextMeshProUGUI ui_itemPriceText;
    
    [SerializeField] private ContainerBonusUI _containerBonusUI;

    private Item _item;

    public void Initialize()
    {
        _containerBonusUI.Initialize();
    }

    public void SetItem(Item item)
    {
        _item = item;

        _containerBonusUI.UpdateData(_item);

        UpdatePanel();
    }

    private void UpdatePanel()
    {
        ui_itemIconImage.sprite = _item.Icon;
        
        ui_itemNameText.text = _item.Name;
        
        ui_itemRareText.text = _item.Rarity.ToString();
        ui_itemRareText.color = _rarityColors.GetColor(_item.Rarity);

        ui_itemPriceText.text = _item.Price.ToString();
    }
}