using UnityEngine;
using UnityEngine.UI;

public class UI_UnitHPIndicator : UI_Panel
{
    [SerializeField] private Slider ui_hpSlider;
    [SerializeField] private U_Mortal unit;
    private Camera _camera;

    void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (ui_hpSlider == null || unit == null || _camera == null) return;

        transform.rotation = _camera.transform.rotation;
        ui_hpSlider.value = unit.GetCurrentHP() / unit.GetMaxHP();
    }
}