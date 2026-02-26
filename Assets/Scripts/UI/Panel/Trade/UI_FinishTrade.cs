using UnityEngine;
using UnityEngine.UI;

public class UI_FinishTrade : UI_Panel 
{
    [SerializeField] private Button ui_yesButton;
    [SerializeField] private Button ui_noButton;

    public void Initialize(FinishTrade finishTrade)
    {
        ui_yesButton.onClick.RemoveAllListeners();
        ui_yesButton.onClick.AddListener(() => 
            {
                finishTrade.Finish();
                Hide();
            }
        );

        ui_noButton.onClick.RemoveAllListeners();
        ui_noButton.onClick.AddListener(() => Hide());
    }
}