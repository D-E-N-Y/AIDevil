using UnityEngine;
using UnityEngine.UI;

public class WorldResource : WorldPickup
{
    [SerializeField] private ResourceIcons _resourceIcons;

    [SerializeField] private ResourceType _resource;
    [SerializeField] private int _amount;
    
    [SerializeField] private Image ui_icon;

    public void Initialize(ResourceType resource, int amout)
    {
        _resource = resource;
        _amount = amout;

        ResourceIcon resourceIcon = _resourceIcons.GetResourceIcon(resource);

        ui_icon.sprite = resourceIcon.sprite;
        ui_icon.color = resourceIcon.color;

        gameObject.SetActive(true);
    }

    public override void PickUp(ItemContext context)
    {
        int finalAmount = _amount; 

        if (_resource == ResourceType.Credits)
        {
            float modifier = ((PlayerCharacterStats)context.Stats).MoneyModifier;
            finalAmount = Mathf.RoundToInt(finalAmount * modifier);
        }

        context.Wallet.AddResource(_resource, finalAmount);

        gameObject.SetActive(false);
    }
}