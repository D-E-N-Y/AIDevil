using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ContainerBonusUI : UI_Panel 
{
    [SerializeField] private UI_Bonus _bonusPrefab;
    [SerializeField] private Transform _bonusContainer;

    private List<UI_Bonus> _ui_bonuses;
    private Dictionary<ItemType, Action<Item>> _viewActions;

    private Dictionary<string, float> _spellBonuses;

    public void Initialize()
    {
        _ui_bonuses = new List<UI_Bonus>();

        _viewActions = new Dictionary<ItemType, Action<Item>>()
        {
            { ItemType.Equipment, item => ViewEquipmentBonuses(item as EquipmentItem) },
            { ItemType.Consumable, item => ViewConsumableBonuses(item as ConsumableItem) },
            { ItemType.Spell, item => ViewSpellBonuses(item as SpellItem) }
        };

        _spellBonuses = new Dictionary<string, float>()
        {
            { "Damage", 0 },
            { "Range Attack", 0 },
            { "Cooldown", 0 }
        };

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
        _viewActions[item.Type].Invoke(item);
    }

    private void ViewEquipmentBonuses(EquipmentItem item)
    {
        IsEnoughBonuses(item.Modifiers.Count);

        for (int i = 0; i < item.Modifiers.Count; i++)
        {
            _ui_bonuses[i].Initialize(item.Modifiers[i].stat.ToString(), item.Modifiers[i].value, item.Type);
            _ui_bonuses[i].Show();
        }
    }

    private void ViewConsumableBonuses(ConsumableItem item)
    {
        // IsEnoughBonuses(consumable.Bonuses.Count);

        // for (int i = 0; i < consumable.Bonuses.Count; i++)
        // {
        //     _ui_bonuses[i].Initialize(consumable.Bonuses[i].stat.ToString(), consumable.Bonuses[i].value);
        //     _ui_bonuses[i].Show();
        // }
    }

    private void ViewSpellBonuses(SpellItem item)
    {
        IsEnoughBonuses(_spellBonuses.Count);

        _spellBonuses["Damage"] = item.Spell.Weapon.Damage;
        _spellBonuses["Range Attack"] = item.Spell.RangeAttack();
        _spellBonuses["Cooldown"] = item.Spell.GetCooldown();

        for (int i = 0; i < _spellBonuses.Count; i++)
        {
            string key = _spellBonuses.Keys.ToList()[i];
            
            _ui_bonuses[i].Initialize(key, _spellBonuses[key], item.Type);
            _ui_bonuses[i].Show();
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