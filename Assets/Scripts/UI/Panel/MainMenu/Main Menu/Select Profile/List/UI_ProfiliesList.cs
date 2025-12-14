using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UI_ProfiliesList : UI_Panel 
{
    public Action<Profile> onSelect;
    
    [SerializeField] private UI_Profile ui_profilePrefab;
    [SerializeField] private RectTransform containerUIProfilies;
    private UI_Profile selected_ui_profile;

    private GameInstance _gameInstance;    
    private DB_Profilies _db_profilies;
    
    public void Initialize(GameInstance gameInstance)
    {
        _gameInstance = gameInstance;
        _db_profilies = DataBase.current.Profilies;

        AddSubscriptions();

        UpdateData();
    }

    private void UpdateData()
    {
        selected_ui_profile = null;
        
        List<Profile> _profilies = _db_profilies.GetProfiles();
        
        List<UI_Profile> _ui_profiles = new List<UI_Profile>();
        _ui_profiles = containerUIProfilies.GetComponentsInChildren<UI_Profile>(true).ToList();
        _ui_profiles.ForEach(x => x.Hide());

        int residue = Math.Max(_profilies.Count - _ui_profiles.Count, 0);

        // stop function if session results is none
        if(_profilies.Count <= 0) return;

        if(residue > 0)
        {
            for(int i = 0; i < residue; i++)
            {
                UI_Profile _ui_profile = Instantiate(ui_profilePrefab, containerUIProfilies);
                _ui_profiles.Add(_ui_profile);
            }
        }

        for(int i = 0; i < _profilies.Count; i++)
        {
            _ui_profiles[i].Initialize(_profilies[i]);
            _ui_profiles[i].onSelect += Select;
            _ui_profiles[i].Show();
        }

        if(_gameInstance.IsValidProfile())
        {
            Profile currentProfile = _gameInstance.GetProfile();
            UI_Profile ui_profile = _ui_profiles.Find(x => x.GetProfile().name == currentProfile.name);
            ui_profile.Select();
        }
        else
        {
            _ui_profiles[0].Select();
        }
    }

    private void Select(UI_Profile ui_profile)
    {
        if(selected_ui_profile == ui_profile) return;
        
        if(selected_ui_profile != null)
        {
            selected_ui_profile.UnSelect();
            selected_ui_profile = null;
        }

        selected_ui_profile = ui_profile;
        onSelect?.Invoke(selected_ui_profile.GetProfile());
    }

    protected override void AddSubscriptions()
    {
        base.AddSubscriptions();
        _db_profilies.onUpdateDB += UpdateData;
    }

    protected override void ClearSubscriptions()
    {
        base.ClearSubscriptions();
        _db_profilies.onUpdateDB -= UpdateData;
    }
}