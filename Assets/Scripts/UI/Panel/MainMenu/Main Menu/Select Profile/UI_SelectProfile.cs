using UnityEngine;
using UnityEngine.UI;

public class UI_SelectProfile : UI_Panel
{
    [SerializeField] private Button ui_closeButton;
    [SerializeField] private Button ui_createButton;
    [SerializeField] private Button ui_removeButton;

    [SerializeField] private UI_ProfiliesList ui_profiliesList;

    private GameInstance _gameInstance;
    private UI_ProfiliesMenu _ui_profiliesMenu;

    public void Initialize(GameInstance gameInstance, UI_ProfiliesMenu ui_profiliesMenu, UI_CreateProfile ui_createProfile)
    {
        _gameInstance = gameInstance;
        _ui_profiliesMenu = ui_profiliesMenu;
        
        AddSubscriptions();       

        ui_profiliesList.Initialize(_gameInstance);

        ui_createButton.onClick.RemoveAllListeners();
        ui_createButton.onClick.AddListener(() => ui_createProfile.Show());
        
        UpdateData();
    }

    private void UpdateData()
    {
        ui_closeButton.onClick.RemoveAllListeners();
        ui_closeButton.onClick.AddListener(() => _ui_profiliesMenu.Hide());
        ui_closeButton.interactable = _gameInstance.ProfileManager.HasProfilies();

        ui_removeButton.interactable = _gameInstance.ProfileManager.IsValidProfile();
    }

    private void Select(Profile profile)
    {
        _gameInstance.ProfileManager.SetProfile(profile);

        ui_removeButton.onClick.RemoveAllListeners();
        ui_removeButton.onClick.AddListener(() => _gameInstance.ProfileManager.RemoveProfile(profile));
    }

    protected override void AddSubscriptions()
    {
        base.AddSubscriptions();

        _gameInstance.ProfileManager.onUpdateProfiles += UpdateData;

        ui_profiliesList.onSelect += Select;
    }

    protected override void ClearSubscriptions()
    {
        base.ClearSubscriptions();

        if (_gameInstance != null)
        {
            _gameInstance.ProfileManager.onUpdateProfiles -= UpdateData;
        }

        ui_profiliesList.onSelect -= Select;
    }
}