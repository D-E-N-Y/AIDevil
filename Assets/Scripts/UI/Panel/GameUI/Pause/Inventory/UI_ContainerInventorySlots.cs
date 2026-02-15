using System.Collections.Generic;
using UnityEngine;

public class UI_ContainerInventorySlots : UI_Panel 
{
    private UI_InventorySlot ui_inventorySlotPrefab;
    
    private List<UI_InventorySlot> ui_slots;
    private int currentSlotIndex;

    public void Initiaalize(UI_InventorySlot ui_inventorySlotPrefab)
    {
        this.ui_inventorySlotPrefab = ui_inventorySlotPrefab;
        
        ui_slots = new List<UI_InventorySlot>(GetComponentsInChildren<UI_InventorySlot>());
        currentSlotIndex = 0;
    }

    public void HideAllSlots()
    {
        foreach (UI_InventorySlot slot in ui_slots)
        {
            slot.Hide();
        }
    }

    public void AddSlot(InventorySlot slot)
    {
        if(ui_slots.Count <= currentSlotIndex)
        {
            UI_InventorySlot newSlot = Instantiate(ui_inventorySlotPrefab, transform);
            ui_slots.Add(newSlot);
        }

        ui_slots[currentSlotIndex].Initialize(slot);
        ui_slots[currentSlotIndex].Show();

        currentSlotIndex++;
    }
}