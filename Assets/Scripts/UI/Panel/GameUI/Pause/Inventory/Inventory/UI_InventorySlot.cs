using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_InventorySlot : UI_Panel 
{
    [SerializeField] private Image ui_itemIcon;
    [SerializeField] private TextMeshProUGUI ui_itemCountText;

    public void Initialize(InventorySlot slot)
    {
        ui_itemIcon.sprite = slot.Item.Icon;
        
        if(slot.Count > 1)
        {
            ui_itemCountText.text = slot.Count.ToString();
            ui_itemCountText.gameObject.SetActive(true);
        }
        else
        {
            ui_itemCountText.gameObject.SetActive(false);
        }
    }
}