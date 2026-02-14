using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Trade : UI_Panel 
{
    public event Action onTrade;
    
    [SerializeField] private Image ui_itemIconImage;

    [SerializeField] private TextMeshProUGUI ui_itemNameText;
    [SerializeField] private TextMeshProUGUI ui_itemRareText;
    [SerializeField] private TextMeshProUGUI ui_itemPriceText;
    
    [SerializeField] private Button ui_tradeButton;
    [SerializeField] private Button ui_closePanelButton;

    private Item _item;

    public void Initialize(Item item, bool canBuy)
    {
        _item = item;

        UpdatePanel();
        SetButtons(canBuy);
    }

    private void UpdatePanel()
    {
        ui_itemIconImage.sprite = _item.Icon;
        ui_itemNameText.text = _item.Name;
        ui_itemRareText.text = _item.Rarity.ToString();
        ui_itemPriceText.text = _item.Price.ToString();
    }

    private void SetButtons(bool canBuy)
    {
        ui_tradeButton.interactable = canBuy;
        ui_tradeButton.onClick.RemoveAllListeners();
        ui_tradeButton.onClick.AddListener(() => Trade());

        ui_closePanelButton.onClick.RemoveAllListeners();
        ui_closePanelButton.onClick.AddListener(() => Hide());
    }

    private void Trade()
    {
        onTrade?.Invoke();
        Hide();
    }
}