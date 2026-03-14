using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class UI_InventoryMenu : UI_Panel 
{
    [SerializeField] private UI_Inventory ui_inventory;
    [SerializeField] private UI_Wallet ui_wallet;

    [SerializeField] private Button ui_backButton;

    public void Initialize(Inventory inventory, Wallet wallet, UI_PauseMenu ui_pauseMenu)
    {
        ui_backButton.onClick.RemoveAllListeners();
        ui_backButton.onClick.AddListener(() => {
            ui_pauseMenu.Show();
            Hide();
        });

        ui_inventory.Initialize(inventory);
        ui_wallet.Initialize(wallet);
    }
}