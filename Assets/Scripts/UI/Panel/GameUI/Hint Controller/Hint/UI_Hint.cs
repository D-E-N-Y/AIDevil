using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(UI_HintMovement), typeof(UI_HintVisual), typeof(RectTransform))]
public class UI_Hint : UI_Panel 
{
    private Action<Action> _subscribe;
    private Action<Action> _unsubscribe;
    
    [SerializeField] private Image ui_icon;
    [SerializeField] private RectTransform _visibileObject;

    private HintType _type;
    public HintType Type => _type;

    private bool _isHasTarget;
    public bool IsHasTarget => _isHasTarget;

    private UI_HintMovement _movement;
    private UI_HintVisual _visual;

    public void Initialize(HintType type, Sprite sprite, RectTransform container)
    {
        _type = type;
        ui_icon.sprite = sprite;

        RectTransform self = GetComponent<RectTransform>();

        _movement = GetComponent<UI_HintMovement>();
        _movement.Initialize(container, self);

        _visual = GetComponent<UI_HintVisual>();
        _visual.Initialize(container, self, _visibileObject, _movement);
    }

    public void SetTarget(Vector3 targetPosition, Action<Action> subscribe, Action<Action> unsubscribe)
    {
        _isHasTarget = true;
        
        _movement.SetTargetPosition(targetPosition);
        _movement.StartMove();

        _visual.SetTargetPosition(targetPosition);

        _unsubscribe?.Invoke(Complete);

        _subscribe = subscribe;
        _unsubscribe = unsubscribe;

        _subscribe(Complete);
    }

    private void Complete()
    {
        _unsubscribe?.Invoke(Complete);
        
        _isHasTarget = false;

        _movement.StopMove();

        Hide();
    }
}