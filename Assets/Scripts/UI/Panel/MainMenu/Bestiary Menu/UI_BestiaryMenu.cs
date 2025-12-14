using UnityEngine;
using UnityEngine.UI;

public class UI_BestiaryMenu : UI_Panel
{
    [SerializeField] private Button ui_buttonClose;
    
    [SerializeField] private UI_EnemyList ui_enemyList;
    [SerializeField] private UI_EnemyDescription ui_enemyDescription;
    
    public void Initialize()
    {
        ui_buttonClose.onClick.RemoveAllListeners();
        ui_buttonClose.onClick.AddListener(Hide);

        ui_enemyList.onSelect += ui_enemyDescription.SetUnitInfo;
        
        UpdateData();
    }

    public void UpdateData()
    {
        ui_enemyList.UpdateData();
        
        if(GameInstance.current.GetProfile().bestiaryData.HasAnyDiscoveredEnemies())
        {
            ui_enemyDescription.Show();
        }
        else
        {
            ui_enemyDescription.Hide();
        }
    }

    protected override void AddSubscriptions()
    {
        base.AddSubscriptions();
        ui_enemyList.onSelect += ui_enemyDescription.SetUnitInfo;
    }

    protected override void ClearSubscriptions()
    {
        base.ClearSubscriptions();
        ui_enemyList.onSelect += ui_enemyDescription.SetUnitInfo;
    }
}
