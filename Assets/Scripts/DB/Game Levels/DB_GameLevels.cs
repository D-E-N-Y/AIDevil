using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DB Game Levels", menuName = "DataBase/GameLevels")]
public class DB_GameLevels : ScriptableObject 
{
    [SerializeField] private List<GameLevel> _gameLevels;
    public IReadOnlyList<GameLevel> GameLevels => _gameLevels;

    public GameLevel GetGameLevelByName(string name)
    {
        foreach (GameLevel level in _gameLevels)
        {
            if (level.Name == name)
            {
                return level;
            }
        }

        return null;
    }
}