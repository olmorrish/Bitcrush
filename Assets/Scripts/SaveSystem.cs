using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {

    public static void SaveGame() {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/save.bitcrush";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveState state = new SaveState();
        formatter.Serialize(stream, state);
        stream.Close();
    }

    public static SaveState LoadGame() {
        string path = Application.persistentDataPath + "/save.bitcrush";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveState loadedData = (SaveState)formatter.Deserialize(stream);
            stream.Close();
            return loadedData;
        }
        else {
            Debug.Log("Save file not found.");
            return null;
        }
    }
}
