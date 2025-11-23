using TMPro;
using UnityEngine;

public class UI_CharacterDescription : UI_Panel 
{
    [SerializeField] private TextMeshProUGUI ui_nameText;
    [SerializeField] private TextMeshProUGUI ui_hpText;
    [SerializeField] private TextMeshProUGUI ui_armorText;
    [SerializeField] private TextMeshProUGUI ui_speedText;
    
    public void Initialize()
    {
        
    }

    public void SetCharacterInfo(Player player)
    {
        ui_nameText.text = player.GetName();
        ui_hpText.text = player.GetMaxHP().ToString();
        ui_armorText.text = player.GetArmor().ToString();
        ui_speedText.text = player.GetMoveSpeed().ToString();
    }
}