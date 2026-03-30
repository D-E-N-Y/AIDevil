using TMPro;
using UnityEngine;

public class UI_MoneyAmount : UI_Panel
{
    [SerializeField] private TextMeshProUGUI ui_moneyAmountText;

    private Wallet _wallet;

    public void Initialize(Wallet wallet)
    {
        if(_wallet != null) ClearSubscriptions();
        
        _wallet = wallet;
        AddSubscriptions();

        UpdateMoneyAmountText();
    }

    private void UpdateMoneyAmountText()
    {
        ui_moneyAmountText.text = _wallet.Resources[ResourceType.Credits].ToString();
    }

    protected override void AddSubscriptions()
    {
        base.ClearSubscriptions();

        _wallet.OnResourceAmountChanged += UpdateMoneyAmountText;
    }

    protected override void ClearSubscriptions()
    {
        base.ClearSubscriptions();

        _wallet.OnResourceAmountChanged -= UpdateMoneyAmountText;
    }
}
