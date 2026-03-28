using System.Collections.Generic;

public class SessionResultsProgress
{
    private List<SSesionResult> _sesionResults;
    public IReadOnlyList<SSesionResult> SesionResults => _sesionResults;

    public SessionResultsProgress()
    {
        _sesionResults = new List<SSesionResult>();
    }

    public SessionResultsProgress(List<SSesionResult> sesionResults)
    {
        if (sesionResults == null)
        {
            _sesionResults = new List<SSesionResult>();
        }
        else
        {
            _sesionResults = sesionResults;
        }
    }

    public void AddSessionResult(SSesionResult result)
    {
        _sesionResults.Add(result);
    }

    public bool HasSessionResults()
    {
        return _sesionResults.Count > 0;
    }
}