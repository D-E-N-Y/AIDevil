using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UI_EnemyList : UI_Panel 
{
    public Action<Enemy> onSelect;
    
    [SerializeField] private UI_Enemy ui_enemyPrefab;
    [SerializeField] private Transform contentTransform;

    [SerializeField] private TextMeshProUGUI ui_progressText;

    private UI_Enemy selected_ui_enemy;

    public void UpdateData()
    {
        List<Enemy> discoveredEnemies = DataBase.current.Enemies.GetEnemiesByNames(GameInstance.current.GetProfile().bestiaryData.discoveredEnemiesNames);
        List<Enemy> allEnemies = DataBase.current.Enemies.GetAllEnemies();

        UpdateList(discoveredEnemies);
        UpdateProgress(allEnemies.Count, discoveredEnemies.Count);
    }

    private void UpdateList(List<Enemy> enemies)
    {
        selected_ui_enemy = null;
        
        List<UI_Enemy> ui_enemies = new List<UI_Enemy>();
        ui_enemies = contentTransform.GetComponentsInChildren<UI_Enemy>(true).ToList();

        int residue = Math.Abs(ui_enemies.Count - enemies.Count);
        if(residue > 0)
        {
            for(int i = 0; i < residue; i++)
            {
                UI_Enemy ui_enemy = Instantiate(ui_enemyPrefab, contentTransform);
                ui_enemies.Add(ui_enemy);
            }
        }

        ui_enemies.ForEach(x => x.Hide());

        for(int i = 0; i < enemies.Count; i++)
        {
            ui_enemies[i].Initialize(enemies[i]);
            ui_enemies[i].onSelect += Select;
            ui_enemies[i].Show();
        }

        ui_enemies[0].Select();
    }

    private void UpdateProgress(int totalCount, int discoveredCount)
    {
        int percentage = (int)(((float)discoveredCount / (float)totalCount) * 100f);
        ui_progressText.text = percentage.ToString();
    }

    private void Select(UI_Enemy ui_enemy)
    {
        if(selected_ui_enemy == ui_enemy) return;

        if(selected_ui_enemy != null)
        {
            selected_ui_enemy.UnSelect();
        }
        
        selected_ui_enemy = ui_enemy;

        onSelect?.Invoke(selected_ui_enemy.GetEnemy());
    }
}