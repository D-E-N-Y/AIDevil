using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UI_Enemy : UI_Panel 
{
    public Action<UI_Enemy> onSelect;

    [SerializeField] private Image ui_selectImage;
    [SerializeField] private Image ui_iconImage;

    private Color selectColor;
    private Color defaultColor;

    private Enemy _enemy;

    public void Initialize(Enemy enemy)
    {
        _enemy = enemy;

        onSelect = null;

        selectColor = new Vector4(255f / 255f, 243f / 255f, 208f / 255f, 1f);
        defaultColor = new Vector4(32f / 255f, 18f / 255f, 6f / 255f, 1f);

        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(Select);

        UnSelect();
    }

    public void Select()
    {
        ui_selectImage.gameObject.SetActive(true);
        ui_iconImage.color = selectColor;
        
        onSelect?.Invoke(this);
    }

    public void UnSelect()
    {
        ui_selectImage.gameObject.SetActive(false);
        ui_iconImage.color = defaultColor;
    }

    public Enemy GetEnemy() => _enemy;
}