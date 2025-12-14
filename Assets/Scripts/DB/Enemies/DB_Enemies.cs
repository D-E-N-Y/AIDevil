using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DataBase/Enemies")] 
public class DB_Enemies : ScriptableObject
{
    [SerializeField] private List<Enemy> enemies;
    
    public Enemy GetEnemyByName(string name)
    {
        return enemies.Find(enemy => enemy.GetUnitName() == name);
    }

    public List<Enemy> GetAllEnemies()
    {
        return enemies;
    }
}