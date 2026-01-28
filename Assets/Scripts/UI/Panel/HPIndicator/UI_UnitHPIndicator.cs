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
        unit.OnHpChanged += UpdateHP;
        UpdateHP();

        _camera = Camera.main;
    }

    private void UpdateHP()
    {
        ui_hpSlider.value = (float)unit.CurrentHP / (float)unit.MaxHP;
    }

    private void Update()
    {
        if (_camera != null) transform.rotation = _camera.transform.rotation;
    }
}