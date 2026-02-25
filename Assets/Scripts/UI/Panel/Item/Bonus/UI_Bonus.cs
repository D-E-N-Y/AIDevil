using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Bonus : UI_Panel 
{
    // [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _valueText;

    public void Initialize(string name, float value, ItemType type)
    {
        // _icon.sprite = statIcon.icon;
        // _icon.color = statIcon.color;

        _nameText.text = name;

        if (type == ItemType.Consumable)
        {
            ConsumableValue(value);
        }
        else if (type == ItemType.Spell)
        {
            SpellValue(value);
        }
        else if (type == ItemType.Equipment)
        {
            EquipmentValue(value);
        }
    }

    private void ConsumableValue(float value)
    {
        _valueText.text = "";
    }

    private void SpellValue(float value)
    {
        _valueText.text = "";

        _valueText.text = value.ToString();
        _valueText.color = Color.green;
    }

    private void EquipmentValue(float value)
    {
        _valueText.text = "";
        
        if(Mathf.Abs(value) < 1 && Mathf.Abs(value) > 0)
        {
            _valueText.text = (Mathf.Abs(value) * 100).ToString("F0") + "%";
        }
        else
        {
            _valueText.text = Mathf.Abs(value).ToString();
        }
        
        
        if (value > 0)
        {
            _valueText.text = "+" + _valueText.text;
            _valueText.color = Color.green;
        }
        else if (value < 0)
        {
            _valueText.text = "-" + _valueText.text;
            _valueText.color = Color.red;
        }
        else
        {
            _valueText.text = "0";
            _valueText.color = Color.white;
        }
    }
}