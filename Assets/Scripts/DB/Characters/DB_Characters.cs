using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DataBase/Characters")] 
public class DB_Characters : ScriptableObject
{
    [SerializeField] private List<PlayerCharacter> _characters;

    public List<PlayerCharacter> GetCharacters() => _characters; 

    public PlayerCharacter GetCharacterByName(string name)
    {
        return _characters.Find(character => character.GetName() == name);
    }

    public PlayerCharacter GetCharacterByID(string id)
    {
        return _characters.Find(character => character.ID == id);
    }
}