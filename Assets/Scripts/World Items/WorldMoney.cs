using UnityEngine;
using UnityEngine.UI;

public class WorldMoney : WorldPickup
{
    [SerializeField] private int _amount;
    // [SerializeField] private Image _iconImage;

    public void Initialize(int amoutMoney)
    {
        _amount = amoutMoney;

        gameObject.SetActive(true);
    }

    public override void PickUp(ItemContext context)
    {
        float modifier = ((PlayerCharacterStats)context.Stats).MoneyModifier;
        int finalAmount = Mathf.RoundToInt(_amount * modifier);

        context.Wallet.AddMoney(finalAmount);

        gameObject.SetActive(false);
    }
}