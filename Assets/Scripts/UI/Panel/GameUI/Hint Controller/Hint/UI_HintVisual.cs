using UnityEngine;

public class UI_HintVisual : MonoBehaviour
{
    private RectTransform _container;
    private RectTransform _self;
    private RectTransform _visibileObject;

    private bool isVisible;
    
    private Camera _camera;

    private Vector3 _targetPosition;

    private UI_HintMovement _movement;

    public void Initialize(RectTransform container, RectTransform self, RectTransform visibileObject,  UI_HintMovement movement)
    {
        _container = container;
        _self = self;
        _visibileObject = visibileObject;

        _movement = movement;
        
        _camera = Camera.main;
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        _targetPosition = targetPosition;
    }

    private void LateUpdate()
    {
        if (_movement.IsMoving)
        {
            VisibleTarget(_movement.CurrentPos);
            CorrectSizeByDistance();
        }
    }

    private void SetVisibility(bool isVisible)
    {
        _visibileObject.gameObject.SetActive(!isVisible);
    }

    private void VisibleTarget(Vector2 pos)
    {
        if(IsCanSee(pos))
        {
            if (isVisible)
            {
                isVisible = false;
                SetVisibility(isVisible);
            }
        }
        else
        {
            if (!isVisible)
            {
                isVisible = true;
                SetVisibility(isVisible);
            }
        }
    }

    private bool IsCanSee(Vector2 pos)
    {
        float halfWidth = _container.rect.width * 0.5f;
        float halfHeight = _container.rect.height * 0.5f;
        
        return pos.x < -halfWidth * 0.9 || pos.x > halfWidth * 0.9 || pos.y < -halfHeight * 0.9 || pos.y > halfHeight * 0.9;
    }

    private void CorrectSizeByDistance()
    {
        float distance = Vector3.Distance(_camera.transform.position, _targetPosition);
        
        float t = Mathf.InverseLerp(30, 100, distance);
        float scale = Mathf.Lerp(1f, 0.5f, t);

        _self.localScale = Vector3.one * scale;
    }
}