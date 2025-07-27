using UnityEngine;
using UnityEngine.UI;

public class UI_BossHPIndicator : UI_Panel
{
    [SerializeField] private Slider ui_hpSlider;
    [SerializeField] private Boss boss;
    private Camera _camera;

    void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (ui_hpSlider == null || boss == null || _camera == null) return;

        transform.rotation = _camera.transform.rotation;
        ui_hpSlider.value = boss.GetCurrentHP() / boss.GetMaxHP();
    }
}