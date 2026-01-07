using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UI_SessionResultsList : UI_Panel
{
    public Action<SSesionResult> onSelect;
    
    [SerializeField] private UI_SessionResult ui_sessionResultPrefab;
    [SerializeField] private RectTransform containerUISessionResults;
    private UI_SessionResult selected_ui_sessionResult;

    private GameInstance _gameInstance;
    
    public void Initialize(GameInstance gameInstance)
    {
        _gameInstance = gameInstance;

        UpdateData();
    }

    public void UpdateData()
    {
        selected_ui_sessionResult = null;
        IReadOnlyList<SSesionResult> _sessionResults = _gameInstance.GetSessionResultsCurrentProfile();
        
        List<UI_SessionResult> _ui_sessionResults = new List<UI_SessionResult>();
        _ui_sessionResults = containerUISessionResults.GetComponentsInChildren<UI_SessionResult>(true).ToList();
        _ui_sessionResults.ForEach(x => x.Hide());

        // stop function if session results is none
        if(_sessionResults.Count <= 0) return;

        int residue = Math.Abs(_ui_sessionResults.Count - _sessionResults.Count);
        if(residue > 0)
        {
            for(int i = 0; i < residue; i++)
            {
                UI_SessionResult _ui_sessionResult = Instantiate(ui_sessionResultPrefab, containerUISessionResults);
                _ui_sessionResults.Add(_ui_sessionResult);
            }
        }

        for(int i = 0; i < _sessionResults.Count; i++)
        {
            _ui_sessionResults[i].Initialize(_sessionResults[i]);
            _ui_sessionResults[i].onSelect += Select;
            _ui_sessionResults[i].Show();
        }

        _ui_sessionResults[0].Select();
    }

    private void Select(UI_SessionResult ui_sessionResult)
    {
        if(selected_ui_sessionResult == ui_sessionResult) return;
        
        if(selected_ui_sessionResult != null)
        {
            selected_ui_sessionResult.UnSelect();
            selected_ui_sessionResult = null;
        }

        selected_ui_sessionResult = ui_sessionResult;
        onSelect?.Invoke(selected_ui_sessionResult.GetSesionResult());
    }
}
