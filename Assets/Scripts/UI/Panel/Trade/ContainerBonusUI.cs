using System.Collections.Generic;
using UnityEngine;

public class ContainerBonusUI : UI_Panel 
{
    [SerializeField] private UI_Bonus _bonusPrefab;
    [SerializeField] private Transform _bonusContainer;

    private List<UI_Bonus> _ui_bonuses;

    public void Initialize()
    {
        _ui_bonuses = new List<UI_Bonus>();

        foreach (Transform child in _bonusContainer)
        {
            UI_Bonus bonusUI = child.GetComponent<UI_Bonus>();
            if (bonusUI != null)
            {
                _ui_bonuses.Add(bonusUI);
                bonusUI.Hide();
            }
        }
    }

    public void UpdateData(Item item)
    {
        _ui_bonuses.ForEach(b => b.Hide());

        if (item is EquipmentItem equipment)
        {
            IsEnoughBonuses(equipment.Modifiers.Count);

            for (int i = 0; i < equipment.Modifiers.Count; i++)
            {
                _ui_bonuses[i].Initialize(equipment.Modifiers[i].stat.ToString(), equipment.Modifiers[i].value);
                _ui_bonuses[i].Show();
            }
        }
    }

    private void IsEnoughBonuses(int count)
    {
        if (_ui_bonuses.Count < count)
        {
            int toCreate = count - _ui_bonuses.Count;

            if(toCreate <= 0) return;

            for (int i = 0; i < toCreate; i++)
            {
                UI_Bonus newBonus = Instantiate(_bonusPrefab, _bonusContainer);
                _ui_bonuses.Add(newBonus);
                newBonus.Hide();
            }
        }
    }
}