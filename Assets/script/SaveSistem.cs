using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSistem
{
    
    public static void SaveLevel(int level)
    {

        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/level.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        LevelData data = new LevelData(level);

        formatter.Serialize(stream, data);
        stream.Close();

    }

    public static LevelData LoadData()
    {

        string path = Application.persistentDataPath + "/level.fun";

        Debug.Log(path);

        if (File.Exists(path))
        {

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            LevelData data = formatter.Deserialize(stream) as LevelData;
            stream.Close();

            return data;

        }
        else
        {

            //Debug.LogError("Sava file not found in " + path);
            LevelData data = new LevelData(0);
            return data;

        }
    }
    
}
