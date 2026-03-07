using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Trade : UI_Panel 
{
    public event Action onTrade;

    [SerializeField] private UI_Item ui_item;
    [SerializeField] private Button ui_tradeButton;

    [SerializeField] private TextMeshProUGUI ui_tradeText;

    private string canBayText = "BUY";
    private string notEnoughtMoneyText = "NOT ENOUGH MONEY";
    private string maxSpellsCountText = "MAX SPELLS COUNT REACHED";


    public void Initialize()
    {
        ui_item.Initialize();
    }

    public void UpdatePanel(Item item, bool enoughMoney, bool maxSpellsCount)
    {
        ui_item.SetItem(item);

        if (!enoughMoney)
        {
            ui_tradeText.text = notEnoughtMoneyText;
        }
        else if (!maxSpellsCount)
        {
            ui_tradeText.text = maxSpellsCountText;
        }
        else
        {
            ui_tradeText.text = canBayText;
        }

        SetButtons(enoughMoney, maxSpellsCount);
    }

    private void SetButtons(bool enoughMoney, bool maxSpellsCount)
    {
        ui_tradeButton.interactable = enoughMoney && maxSpellsCount;
        ui_tradeButton.onClick.RemoveAllListeners();
        ui_tradeButton.onClick.AddListener(() => Trade());
    }

    private void Trade()
    {
        onTrade?.Invoke();
        Hide();
    }
}