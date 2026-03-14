using System;
using UnityEngine;

public class UI_HintMovement : MonoBehaviour 
{
    private RectTransform _container;
    private RectTransform _self;

    private Vector3 _targetPosition;

    private bool _isMoving;
    public bool IsMoving => _isMoving;

    private Camera _camera;

    private Vector2 _currentPos;
    public Vector2 CurrentPos => _currentPos;

    public void Initialize(RectTransform container, RectTransform self)
    {
        _container = container;
        _self = self;

        InitializeRectTransform();

        _camera = Camera.main;
    }

    private void InitializeRectTransform()
    {
        _self.pivot = new Vector2(0.5f, 0.5f);

        _self.anchorMin = new Vector2(0.5f, 0.5f);
        _self.anchorMax = new Vector2(0.5f, 0.5f);

        _self.anchoredPosition = Vector2.zero;
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        _targetPosition = targetPosition;
    }

    public void StartMove()
    {
        _isMoving = true;
    }

    public void StopMove()
    {
        _isMoving = false;
    }

    void LateUpdate()
    {
        if (!IsMoving) return;

        Vector3 screenPos = _camera.WorldToScreenPoint(_targetPosition);
        
        bool behind = screenPos.z < 0;

        if (behind)
        {
            screenPos.x = Screen.width - screenPos.x;
            screenPos.y = Screen.height - screenPos.y;
        }

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _container, 
            screenPos, 
            null, 
            out Vector2 localPoint
        );

        _currentPos = GetCurrentPosition(localPoint);
        _self.anchoredPosition = _currentPos;
    }

    private Vector2 GetCurrentPosition(Vector2 position)
    {
        float halfWidth = _container.rect.width * 0.5f;
        float halfHeight = _container.rect.height * 0.5f;

        float x = Mathf.Clamp(position.x, -halfWidth, halfWidth);
        float y = Mathf.Clamp(position.y, -halfHeight, halfHeight);

        return new Vector2(x, y);
    }
}