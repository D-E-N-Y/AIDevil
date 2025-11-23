using UnityEngine;

public class GameInstance : MonoBehaviour
{
    public static GameInstance current;

    [SerializeField] private DB_Characters db_characters;

    private Player _player;

    public void Initialize()
    {
        current = this;
        DontDestroyOnLoad(this);

        _player = null;
    }

    public void SetPlayer(Player player)
    {
        if (player == null)
        {
            Debug.Log("!!! select player is null");
            return;
        }

        _player = player;
    }

    public Player GetPlayer() => _player;

    public DB_Characters DBCharacters() => db_characters;
}