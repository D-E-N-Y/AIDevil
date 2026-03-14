using System;
using System.Collections.Generic;
using UnityEngine;

public class UI_HintController : UI_Panel 
{
    [SerializeField] private HintIcons _hintIcons;

    [SerializeField] private UI_Hint ui_hintPrefab;
    [SerializeField] private RectTransform _containerUIHints;

    private Dictionary<HintType, List<UI_Hint>> ui_hints;

    public void Initialize()
    {
        HideAllChildrenObjects();
        InitilizeDictionary();
    }

    private void HideAllChildrenObjects()
    {
        for (int i = 0; i < _containerUIHints.childCount; i++)
        {
            _containerUIHints.GetChild(i).gameObject.SetActive(false);;
        }
    }

    private void InitilizeDictionary()
    {
        ui_hints = new Dictionary<HintType, List<UI_Hint>>();

        foreach (HintType type in Enum.GetValues(typeof(HintType)))
        {
            ui_hints[type] = new List<UI_Hint>();
        }
    }

    public void ShowHint(HintType type, Vector3 targetPosition, Action<Action> subscribe, Action<Action> unsubscribe)
    {
        UI_Hint ui_hint = GetFreeUIHint(type);
        ui_hint.SetTarget(targetPosition, subscribe, unsubscribe);
        ui_hint.Show();
    }

    private UI_Hint GetFreeUIHint(HintType type)
    {
        foreach (UI_Hint hint in ui_hints[type])
        {
            if (!hint.IsHasTarget)
            {
                return hint;
            }
        }

        return CreateNewHint(type);
    }

    private UI_Hint CreateNewHint(HintType type)
    {
        UI_Hint ui_hint = Instantiate(ui_hintPrefab, _containerUIHints);
        ui_hint.Initialize(type, _hintIcons.GetHintSpriteByType(type), _containerUIHints);
        
        ui_hints[type].Add(ui_hint);

        return ui_hint;
    }
}