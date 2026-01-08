using UnityEngine;
using UnityEngine.UI;

public class UI_SessionResultsMenu : UI_Panel
{
    [SerializeField] private Button ui_closeButton;
    
    [SerializeField] private UI_SessionResultsList ui_sessionResultsList;
    [SerializeField] private UI_SessionResultsDescription ui_sessionResultsDescription;
    
    public void Initialize(GameInstance gameInstance)
    {
        ui_sessionResultsList.onSelect += Select;
        ui_sessionResultsList.Initialize(gameInstance);

        ui_closeButton.onClick.RemoveAllListeners();
        ui_closeButton.onClick.AddListener(() => Hide()); 
    }

    private void Select(SSesionResult result)
    {
        ui_sessionResultsDescription.SetResult(result);
    }
}
