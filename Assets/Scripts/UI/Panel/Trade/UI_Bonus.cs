using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Bonus : UI_Panel 
{
    // [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _valueText;

    public void Initialize(string name, float value)
    {
        // _icon.sprite = statIcon.icon;
        // _icon.color = statIcon.color;

        _nameText.text = name;

        CorrectValueText(value);
    }

    private void CorrectValueText(float value)
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