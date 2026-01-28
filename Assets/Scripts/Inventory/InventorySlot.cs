using System;

public class InventorySlot
{
    public event Action OnSlotChanged;
    
    private Item _item;
    public Item Item => _item;

    private int _count;
    public int Count => _count;

    private ItemContext _context;
    public ItemContext Context => _context;

    public InventorySlot(ItemContext context)
    {
        _context = context;

        _item = null;
        _count = 0;
    }

    public InventorySlot(ItemContext context, Item item)
    {
        _context = context;

        SetItem(item);
    }

    public InventorySlot(ItemContext context, Item item, int count)
    {
        _context = context;
        
        SetItem(item, count);
    }

    public void SetItem(Item newItem, int count = 1)
    {
        if(_item != null)
        {
            RemoveItem(_count);
        }
        
        _item = newItem;
        _count = count <= 0 ? 1 : count;

        for (int i = 0; i < count; i++)
        {
            _item.Apply(_context);
        }

        OnSlotChanged?.Invoke();
    }

    public void AddItem(int amount = 1)
    {
        if(_item == null) return;

        _count += amount <= 0 ? 1 : amount;

        for (int i = 0; i < amount; i++)
        {
            _item.Apply(_context); 
        }

        OnSlotChanged?.Invoke();
    }

    public void RemoveItem(int amount = 1)
    {
        if(_item == null) return;
        
        amount = amount <= 0 ? 1 : amount;
        amount = amount > _count ? _count : amount;

        _count -= amount;

        for (int i = 0; i < amount; i++)
        {
            _item.Remove(_context); 
        }

        if (_count <= 0)
        {
            _count = 0;
            _item = null;
        }

        OnSlotChanged?.Invoke();
    }

    public bool IsEmpty() => _item == null;
}