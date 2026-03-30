
using System.Collections.Generic;
using UnityEngine;

public class GameLevelsProgress
{
    private HashSet<string> _completedLevelsID;
    public HashSet<string> CompletedLevels => _completedLevelsID;

    public GameLevelsProgress()
    {
        _completedLevelsID = new HashSet<string>();
    }

    public GameLevelsProgress(HashSet<string> completedLevelsID)
    {
        if (completedLevelsID == null)
        {
            _completedLevelsID = new HashSet<string>();
        }
        else
        {
            _completedLevelsID = new HashSet<string>(completedLevelsID);
        }        
    }

    public void AddGameLevel(string levelID)
    {
        _completedLevelsID.Add(levelID);
    }

    public bool IsGameLevelCompleted(string levelID)
    {
        return _completedLevelsID.Contains(levelID);
    }
}