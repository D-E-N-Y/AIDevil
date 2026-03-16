using UnityEngine;
using UnityEngine.UI;

public class UI_BestiaryMenu : UI_Panel
{
    [SerializeField] private Button ui_buttonClose;
    
    [SerializeField] private UI_EnemyList ui_enemyList;
    [SerializeField] private UI_EnemyDescription ui_enemyDescription;
    
    private GameInstance _gameInstance;

    public void Initialize(GameInstance gameInstance)
    {
        ClearSubscriptions();
        
        _gameInstance = gameInstance;
        
        ui_buttonClose.onClick.RemoveAllListeners();
        ui_buttonClose.onClick.AddListener(Hide);

        ui_enemyDescription.Initialize();

        ui_enemyList.Initialize(gameInstance);
        ui_enemyList.onSelect += ui_enemyDescription.SetUnitInfo;
        
        AddSubscriptions();

        UpdateData();
    }

    private void UpdateData()
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
        _gameInstance.onCurrentProfileChanged += UpdateData;
        ui_enemyList.onSelect += ui_enemyDescription.SetUnitInfo;
    }

    protected override void ClearSubscriptions()
    {
        base.ClearSubscriptions();
        
        if (_gameInstance != null)
        {
            _gameInstance.onCurrentProfileChanged -= UpdateData;
        }

        if (ui_enemyList != null)
        {
            ui_enemyList.onSelect += ui_enemyDescription.SetUnitInfo;
        }
    }
}
