using UnityEngine;
using UnityEngine.UI;

public class UI_HPIndicator : UI_Panel
{
    [SerializeField] private Slider ui_hpSlider;
    private IHealth _health;
    private Camera _camera;


    public void Initialize(IHealth health)
    {
        _health = health;
        health.OnHpChanged += UpdateHP;
        UpdateHP();

        _camera = Camera.main;
    }

    private void UpdateHP()
    {
        ui_hpSlider.value = (float)_health.CurrentHP / (float)_health.MaxHP;
    }

    private void Update()
    {
        if (_camera != null) transform.rotation = _camera.transform.rotation;
    }
}