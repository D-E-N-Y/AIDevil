using TMPro;
using UnityEngine;

public class UI_EnemyDescription : UI_Panel 
{
    [SerializeField] private TextMeshProUGUI ui_nameText;
    [SerializeField] private TextMeshProUGUI ui_hpText;
    // [SerializeField] private TextMeshProUGUI ui_armorText;
    [SerializeField] private TextMeshProUGUI ui_speedText;
    [SerializeField] private UI_SpellsList ui_spellsList;
    // [SerializeField] private TextMeshProUGUI ui_descriptionText;

    public void SetUnitInfo(Enemy enemy)
    {
        ui_nameText.text = enemy.GetName();
        ui_hpText.text = enemy.GetStats().MaxHP.ToString();
        // ui_armorText.text = enemy.GetArmor().ToString();
        ui_speedText.text = enemy.GetStats().BaseMoveSpeed.ToString();
        ui_spellsList.SetInfo(enemy.GetSpells());
    }
}