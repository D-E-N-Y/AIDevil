using System.Collections.Generic;

[System.Serializable]
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
        UpdateDB();
    }

    public void RemoveResult(SSesionResult result)
    {
        _sessionResults.Remove(result);
        UpdateDB();
    }

    private void UpdateDB()
    {
        // SaveLoadSystem.current.SaveGame(
        //     new SaveData(
        //         GameInstance.current.DBProfilies(), 
        //         GameInstance.current.GetProfile()
        //     )
        // );
    }

    public List<SSesionResult> GetSessionResults() => _sessionResults;
    public bool HasRecords() => _sessionResults.Count > 0;
}