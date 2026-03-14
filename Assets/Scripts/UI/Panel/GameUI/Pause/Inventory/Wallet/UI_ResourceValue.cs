using TMPro;
using UnityEngine;

public class UI_ResourceValue : UI_Resource 
{
    [SerializeField] private TextMeshProUGUI ui_valueText;

    public void SetValue(int value)
    {
        ui_valueText.text = value.ToString();
    }
}