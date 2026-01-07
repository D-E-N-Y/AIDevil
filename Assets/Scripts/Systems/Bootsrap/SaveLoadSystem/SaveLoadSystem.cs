using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.IO;

public class SaveLoadSystem
{
    private string fileName = "savegame.dat";
    private string FilePath =>
        Path.Combine(Application.persistentDataPath, fileName);
    
    private SaveData _saveData;

    public void SaveGame(SaveData saveData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream stream = new FileStream(FilePath, FileMode.Create))
        {
            formatter.Serialize(stream, saveData);
        }
    }

    public SaveData LoadGame()
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
}