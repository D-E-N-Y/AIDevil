using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DataBase/Enemies")] 
public class DB_Enemies : ScriptableObject
{
    [SerializeField] private List<Enemy> enemies;
    
    public Enemy GetEnemyByName(string name)
    {
        return enemies.Find(enemy => enemy.GetName() == name);
    }

    public List<Enemy> GetAllEnemies()
    {
        return enemies;
    }

    public List<Enemy> GetEnemiesByNames(List<string> names)
    {
        List<Enemy> selectedEnemies = new List<Enemy>();
        foreach (string name in names)
        {
            Enemy enemy = GetEnemyByName(name);
            if (enemy != null)
            {
                selectedEnemies.Add(enemy);
            }
        }
        return selectedEnemies;
    }
}