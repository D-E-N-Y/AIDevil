using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.IO;

public class SaveLoadSystem : MonoBehaviour 
{
    public static SaveLoadSystem current;

    private string fileName = "savegame.dat";
    private string FilePath =>
        Path.Combine(Application.persistentDataPath, fileName);
    
    private SaveData _saveData;

    public void Initialize()
    {
        current = this;
        DontDestroyOnLoad(this);
    }

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