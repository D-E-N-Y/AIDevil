using System.Collections.Generic;
using System;
using UnityEngine;

public class Inventory
{
    public event Action OnInventoryChanged;
    
    private Dictionary<ItemType, List<InventorySlot>> _slots;
    public IReadOnlyDictionary<ItemType, List<InventorySlot>> Slots => _slots;

    public IReadOnlyList<InventorySlot> ConsumableSlots => _slots[ItemType.Consumable];
    public IReadOnlyList<InventorySlot> EquipmentSlots => _slots[ItemType.Equipment];
    public IReadOnlyList<InventorySlot> SpellSlots => _slots[ItemType.Spell];

    private int _maxSpellsCount;
    public int MaxSpellsCount => _maxSpellsCount;

    private int _currentSpellsCount;
    public int CurrentSpellsCount => _currentSpellsCount;

    public bool IsSpellSlotsFull => _currentSpellsCount >= _maxSpellsCount;

    private ItemContext _context;
    public ItemContext Context => _context;

    public Inventory(int maxSpellsCount = 4)
    {
        _maxSpellsCount = maxSpellsCount;
        
        _slots = new Dictionary<ItemType, List<InventorySlot>>()
        {
            { ItemType.Consumable, new List<InventorySlot>() },
            { ItemType.Equipment, new List<InventorySlot>() },
            { ItemType.Spell, new List<InventorySlot>() }
        };
    }

    public void SetContext(ItemContext context)
    {
        _context = context;
    }

    public void AddItem(Item item)
    {
        if(item.Type == ItemType.Consumable)
        {
            item.Apply(_context);
            return;
        }
        
        InventorySlot _slot = GetSlotWithItem(item);
        if (_slot == null)
        {
            _slot = new InventorySlot(_context, item);
            _slots[item.Type].Add(_slot);
        }
        else
        {
            _slot.AddItem();
        }

        if (item.Type == ItemType.Spell)
        {
            _currentSpellsCount++;
        }

        OnInventoryChanged?.Invoke();
    }

    private InventorySlot GetSlotWithItem(Item item)
    {
        foreach (var slot in _slots[item.Type])
        {
            if (slot.Item != null && slot.Item == item)
            {
                return slot;
            }
        }
        return null;
    }

    public void AddItems(List<Item> items)
    {
        foreach (var item in items)
        {
            AddItem(item);
        }
    }

    public void AddItems(Item item, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            AddItem(item);
        }
    }

    public void RemoveItem(Item item)
    {
        foreach (var slot in _slots[item.Type])
        {
            if (slot.Item != null && slot.Item == item)
            {
                if (item.Type == ItemType.Spell)
                {
                    _currentSpellsCount--;
                } 
                
                slot.RemoveItem();
                OnInventoryChanged?.Invoke();
                return;
            }
        }
    }

    public List<Item> GetAllItems()
    {
        List<Item> itemList = new List<Item>();
        
        foreach (ItemType type in Enum.GetValues(typeof(ItemType)))
        {
            foreach (var slot in _slots[type])
            {
                if (slot.Item != null)
                {
                    for (int i = 0; i < slot.Count; i++)
                    {
                        itemList.Add(slot.Item);
                    }
                }
            }
        }

        return itemList;
    }

    public void ClearInventory()
    {
        _slots.Clear();
        
        OnInventoryChanged?.Invoke();
    }
}