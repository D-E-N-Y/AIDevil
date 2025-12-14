using System;
using System.Collections.Generic;
using UnityEngine;

public class DataBase : MonoBehaviour 
{
    public static DataBase current;
    
    private DB_Profilies db_profilies;
    [SerializeField] private DB_Characters db_characters;
    [SerializeField] private DB_Enemies db_enemies;
    private DB_SessionResults db_sessionResults;

    public void Initialize()
    {
        current = this;
        DontDestroyOnLoad(this);

        db_profilies = new DB_Profilies();
        db_sessionResults = new DB_SessionResults();
    }

    public DB_Profilies Profilies => db_profilies;
    public DB_Enemies Enemies => db_enemies;
    public DB_Characters Characters => db_characters;
    public DB_SessionResults SessionResults => db_sessionResults;
}