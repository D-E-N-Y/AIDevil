using UnityEngine;
using UnityEngine.UI;

public class UI_UnitHPIndicator : UI_Panel
{
    [SerializeField] private Slider ui_hpSlider;
    private IHealth unit;
    private Camera _camera;


    public void Initialize(IHealth unit)
    {
        this.unit = unit;
        _camera = Camera.main;
    }

    private void Update()
    {
        // has unit to show HP
        if (ui_hpSlider != null || unit != null) ui_hpSlider.value = unit.GetCurrentHP() / unit.GetMaxHP(); ;

        // has camera to corrent show UI
        if (_camera != null) transform.rotation = _camera.transform.rotation;
    }
}