using System.Collections.Generic;
using UnityEngine;

public class DB_SessionResults : MonoBehaviour 
{
    private List<SSesionResult> _sessionResults;

    public void Initialize()
    {
        _sessionResults = new List<SSesionResult>();
    }

    public void AddResult(SSesionResult result)
    {
        _sessionResults.Add(result);
    }

    public void RemoveResult(SSesionResult result)
    {
        _sessionResults.Remove(result);
    }

    public List<SSesionResult> GetSessionResults() => _sessionResults;
    public bool HasRecords() => _sessionResults.Count > 0;
}