using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.IO;

public class SaveLoadSystem
{
    private string fileName = "savegame.dat";
    private string FilePath =>
        Path.Combine(Application.persistentDataPath, fileName);

    private GameInstance _gameInstance;

    public SaveLoadSystem(GameInstance gameInstance)
    {
        _gameInstance = gameInstance;
    }

    private void SaveGame(SaveData saveData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream stream = new FileStream(FilePath, FileMode.Create))
        {
            formatter.Serialize(stream, saveData);
        }
    }

    private SaveData LoadGame()
    {
        // return null;
        
        if (!File.Exists(FilePath))
        {
            return null;
        }

        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream stream = new FileStream(FilePath, FileMode.Open))
        {
            return (SaveData)formatter.Deserialize(stream);
        }
    }

    public SaveData LoadData()
    {
        return LoadGame();
    }

    public void SaveData()
    {
        SaveGame(_gameInstance.ProfileManager.GetData());
    }
}