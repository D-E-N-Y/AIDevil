using TMPro;
using UnityEngine;

public class UI_EnemyDescription : UI_Panel 
{
    [SerializeField] private TextMeshProUGUI ui_nameText;
    [SerializeField] private UI_Stats ui_stats;
    [SerializeField] private UI_SpellsList ui_spellsList;

    public void Initialize()
    {
        ui_spellsList.Initialize();
        ui_stats.Initialize();
    }

    public void SetUnitInfo(Enemy enemy)
    {
        ui_nameText.text = enemy.GetName();

        ui_stats.SetStats(enemy.GetStats());
        ui_stats.UpdateUI();

        ui_spellsList.SetInfo(enemy.GetSpells());
    }
}