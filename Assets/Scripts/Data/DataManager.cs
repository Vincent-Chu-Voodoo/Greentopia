using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Game rule independent data manager
/// </summary>
public sealed class DataManager : Singleton<DataManager>
{
    public string GetBasePath()
    {
        return Application.persistentDataPath;
    }

    public void Save(object dataObj)
    {
        var savePath = Path.Combine(Application.persistentDataPath, dataObj.GetType().ToString());
        Save(dataObj, savePath);
    }

    public void Save(object dataObj, string savePath)
    {
        using (var streamWriter = new StreamWriter(savePath))
        {
            var json = JsonUtility.ToJson(dataObj);
            streamWriter.Write(json);
        }
    }

    public T Load<T>()
    {
        var loadPath = Path.Combine(Application.persistentDataPath, typeof(T).ToString());
        return Load<T>(loadPath);
    }

    public T Load<T>(string loadPath)
    {
        using (var streamReader = new StreamReader(loadPath))
        {
            var json = streamReader.ReadToEnd();
            return JsonUtility.FromJson<T>(json);
        }
    }
}
