using UnityEngine;

public class DataBase : MonoBehaviour 
{
    public static DataBase current;
    
    [SerializeField] private DB_Characters db_characters;
    [SerializeField] private DB_Enemies db_enemies;

    public void Initialize()
    {
        current = this;
        DontDestroyOnLoad(this);
    }

    public DB_Enemies Enemies => db_enemies;
    public DB_Characters Characters => db_characters;
}