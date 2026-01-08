using UnityEngine;

public class DataBase : MonoBehaviour 
{
    [SerializeField] private DB_Characters db_characters;
    [SerializeField] private DB_Enemies db_enemies;

    public void Initialize()
    {
        DontDestroyOnLoad(this);
    }

    public DB_Enemies Enemies => db_enemies;
    public DB_Characters Characters => db_characters;
}