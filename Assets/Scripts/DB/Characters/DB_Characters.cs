using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DB/Characters")] 
public class DB_Characters : ScriptableObject
{
    [SerializeField] private List<Player> _characters;

    public List<Player> GetCharacters() => _characters; 

    public Player GetCharacterByName(string name)
    {
        return _characters.Find(character => character.GetName() == name);
    }
}