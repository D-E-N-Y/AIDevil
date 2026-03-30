using System.Collections.Generic;

public class CharacterProgress
{
    private HashSet<string> _unlockedCharactersID;
    public HashSet<string> UnlockedCharacters => _unlockedCharactersID;

    public CharacterProgress()
    {
        _unlockedCharactersID = new HashSet<string>();
    }

    public CharacterProgress(HashSet<string> unlockedCharactersID)
    {
        if (unlockedCharactersID == null)
        {
            _unlockedCharactersID = new HashSet<string>();
        }
        else
        {
            _unlockedCharactersID = new HashSet<string>(unlockedCharactersID);
        }        
    }

    public void AddCharacter(string characterID)
    {
        _unlockedCharactersID.Add(characterID);
    }

    public bool IsCharacterUnlocked(string characterID)
    {
        return _unlockedCharactersID.Contains(characterID);
    }
}