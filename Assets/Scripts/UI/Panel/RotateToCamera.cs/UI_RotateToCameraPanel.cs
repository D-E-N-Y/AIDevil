using UnityEngine;

public class UI_RotateToCameraPanel : UI_Panel 
{
    private Camera _camera;

    private void Awake() 
    {
        _camera = Camera.main;
    }

    private void Update() 
    {
        transform.rotation = _camera.transform.rotation;
    }
}