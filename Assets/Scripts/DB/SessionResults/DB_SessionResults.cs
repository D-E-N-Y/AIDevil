using System.Collections.Generic;

public class DB_SessionResults 
{
    private List<SSesionResult> _sessionResults;

    public DB_SessionResults()
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