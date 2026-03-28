using UnityEngine;

public class DataBase : MonoBehaviour 
{
    [SerializeField] private DB_Characters db_characters;
    [SerializeField] private DB_UpgradeTrees db_upgradeTrees;
    [SerializeField] private DB_Enemies db_enemies;
    [SerializeField] private DB_Items db_items;
    [SerializeField] private DB_GameLevels db_gameLevels;

    public void Initialize()
    {
        DontDestroyOnLoad(this);
    }

    public DB_Enemies Enemies => db_enemies;
    public DB_Characters Characters => db_characters;
    public DB_UpgradeTrees UpgradeTrees => db_upgradeTrees;
    public DB_Items Items => db_items;
    public DB_GameLevels GameLevels => db_gameLevels;
}