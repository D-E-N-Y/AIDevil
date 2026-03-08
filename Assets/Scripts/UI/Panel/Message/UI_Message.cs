using TMPro;
using UnityEngine;

public class UI_Message : UI_Panel 
{
    [SerializeField] protected TextMeshProUGUI ui_messageText;

    public void SetMessageText(string message)
    {
        ui_messageText.text = message;
    }
}