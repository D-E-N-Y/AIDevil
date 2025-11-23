using System.Collections.Generic;
using UnityEngine;

public class DB_Characters : MonoBehaviour
{
    [SerializeField] private List<Player> _characters;

    public List<Player> GetCharacters() => _characters;
}