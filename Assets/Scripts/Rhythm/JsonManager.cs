using System.IO;
using UnityEngine;

public static class JsonManager<T>
{
    private static string path = Application.dataPath + "/Json";

    public static void Save(T userData, string fileName)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        string _saveJson = JsonUtility.ToJson(userData);
        string _filePath = path + "/" + fileName + ".json";

        File.WriteAllText(_filePath, _saveJson);
    }

    public static T Load(string _fileName)
    {
        string _filePath = path + "/" + _fileName + ".json";

        if (!File.Exists(_filePath))
        {
            Debug.LogError("No such saveFile exists");
            return default(T);
        }

        string saveFile = File.ReadAllText(_filePath);
        T _userData = JsonUtility.FromJson<T>(saveFile);
        return _userData;
    }

    public static void Delete(string _fileName)
    {
        string _filePath = path + "/" + _fileName + ".json";

        if (!File.Exists(_filePath))
        {
            Debug.LogError("No such saveFile exists");
            return;
        }
        File.Delete(_filePath);
    }
}