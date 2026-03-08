using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_Offer : UI_Message 
{
    [SerializeField] private Button ui_yesButton;
    [SerializeField] private Button ui_noButton;

    public void SetActions(Action yesAction, Action noAction)
    {
        ui_yesButton.onClick.RemoveAllListeners();
        ui_yesButton.onClick.AddListener(() => 
            {
                yesAction?.Invoke();
                Hide();
            }
        );

        ui_noButton.onClick.RemoveAllListeners();
        ui_noButton.onClick.AddListener(() => 
            {
                noAction?.Invoke();
                Hide();
            }
        );
    }
}