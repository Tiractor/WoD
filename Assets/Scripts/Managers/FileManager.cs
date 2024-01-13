using System.IO;
using UnityEngine;

public static class FileManager
{
    private static string folderPath = Application.persistentDataPath + "/CharacterData/";

    public static void SaveToFile(string fileName, object data)
    {
        Debug.Log("Data saved into " + folderPath + fileName + ".json");
        string fullPath = folderPath + fileName + ".json";
        string jsonData = JsonUtility.ToJson(data, true);
        Debug.Log("Data is " + jsonData);
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        File.WriteAllText(fullPath, jsonData);
    }

    public static T LoadFromFile<T>(string fileName)
    {
        string fullPath = folderPath + fileName + ".json";

        if (File.Exists(fullPath))
        {
            string jsonData = File.ReadAllText(fullPath);
            return JsonUtility.FromJson<T>(jsonData);
        }

        return default(T);
    }

    public static string[] GetAllCharacterFiles()
    {
        if (Directory.Exists(folderPath))
        {
            return Directory.GetFiles(folderPath, "*.json");
        }
        return new string[0];
    }
}