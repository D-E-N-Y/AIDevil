using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : UI_Panel 
{
    [SerializeField] private Button ui_backButton;

    [SerializeField] private UI_ContainerInventorySlots _spellsContainer;
    [SerializeField] private UI_ContainerInventorySlots _equipmentContainer;

    [SerializeField ] private UI_InventorySlot ui_inventorySlotPrefab;

    private Inventory _inventory;

    public void Initialize(Inventory inventory, UI_PauseMenu ui_pauseMenu)
    {
        _inventory = inventory;
        
        ui_backButton.onClick.RemoveAllListeners();
        ui_backButton.onClick.AddListener(() => {
            ui_pauseMenu.Show();
            Hide();
        });

        _spellsContainer.Initiaalize(ui_inventorySlotPrefab);
        _equipmentContainer.Initiaalize(ui_inventorySlotPrefab);
    }

    public void UpdateData()
    {
        _spellsContainer.HideAllSlots();
        _equipmentContainer.HideAllSlots();
        
        UpdateContainer(_inventory.SpellSlots, _spellsContainer);
        UpdateContainer(_inventory.EquipmentSlots, _equipmentContainer);
    }

    private void UpdateContainer(IReadOnlyList<InventorySlot> slots, UI_ContainerInventorySlots container)
    {
        foreach(InventorySlot _slot in slots)
        {
            container.AddSlot(_slot);
        }
    }

    public override void Show()
    {
        base.Show();
        UpdateData();
    }
}