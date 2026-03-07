using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UI_Enemy : UI_Panel 
{
    public Action<UI_Enemy> onSelect;

    [SerializeField] private Image ui_selectImage;
    [SerializeField] private Image ui_unselectImage;

    private Enemy _enemy;

    public void Initialize(Enemy enemy)
    {
        _enemy = enemy;

        onSelect = null;

        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(Select);

        UnSelect();
    }

    public void Select()
    {
        ui_selectImage.gameObject.SetActive(true);
        ui_unselectImage.gameObject.SetActive(false);
        
        onSelect?.Invoke(this);
    }

    public void UnSelect()
    {
        ui_selectImage.gameObject.SetActive(false);
        ui_unselectImage.gameObject.SetActive(true);
    }

    public Enemy GetEnemy() => _enemy;
}