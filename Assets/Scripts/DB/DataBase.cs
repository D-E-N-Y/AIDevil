using UnityEngine;

public class DataBase : MonoBehaviour 
{
    [SerializeField] private DB_Characters db_characters;
    [SerializeField] private DB_Enemies db_enemies;
    [SerializeField] private DB_Items db_items;

    public void Initialize()
    {
        DontDestroyOnLoad(this);
    }

    public DB_Enemies Enemies => db_enemies;
    public DB_Characters Characters => db_characters;
    public DB_Items Items => db_items;
}