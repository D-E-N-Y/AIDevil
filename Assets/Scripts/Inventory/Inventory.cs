using System;
using System.Collections.Generic;

public class Inventory
{
    public event Action OnInventoryChanged;
    
    private List<InventorySlot> slots;
    public IReadOnlyList<InventorySlot> Slots => slots;

    private ItemContext _context;
    public ItemContext Context => _context;

    public Inventory(ItemContext context)
    {
        _context = context;

        slots = new List<InventorySlot>();
    }

    public void AddItem(Item item)
    {
        foreach (var slot in slots)
        {
            if (slot.Item != null && slot.Item == item)
            {
                slot.AddItem();
                OnInventoryChanged?.Invoke();
                return;
            }
        }

        var newSlot = new InventorySlot(_context, item);
        slots.Add(newSlot);
        OnInventoryChanged?.Invoke();
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
        foreach (var slot in slots)
        {
            if (slot.Item != null && slot.Item == item)
            {
                slot.RemoveItem();
                OnInventoryChanged?.Invoke();
                return;
            }
        }
    }

    public List<Item> GetAllItems()
    {
        List<Item> itemList = new List<Item>();
        foreach (var slot in slots)
        {
            if (slot.Item != null)
            {
                for (int i = 0; i < slot.Count; i++)
                {
                    itemList.Add(slot.Item);
                }
            }
        }
        return itemList;
    }

    public void ClearInventory()
    {
        slots.Clear();
        OnInventoryChanged?.Invoke();
    }
}