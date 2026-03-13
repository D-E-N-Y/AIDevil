using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Resource : UI_Panel 
{
    [SerializeField] private ResourceIcons _resourceIcons;
    
    [SerializeField] private Image ui_icon;
    [SerializeField] private TextMeshProUGUI ui_nameText;
    
    public void Initialize(ResourceType resource)
    {
        ui_nameText.text = resource.ToString();

        ResourceIcon resourceIcon = _resourceIcons.GetResourceIcon(resource);

        ui_icon.sprite = resourceIcon.sprite;
        ui_icon.color = resourceIcon.color;
    }
}