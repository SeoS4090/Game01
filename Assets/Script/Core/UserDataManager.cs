using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class UserDataManager : MonoBehaviour
{
    Dictionary<string, string> Data = new Dictionary<string, string>();

    public string GetData(string DataName)
    {
        if(Data.Count <= 0)
        {
            var SavedData = File.ReadAllText($"{Application.persistentDataPath}/UserData.txt");
            Data = JsonUtility.FromJson<Dictionary<string, string>>(SavedData);
        }



        if (DataName == null)
            DataName = "";
        if (Data.ContainsKey(DataName))
            return Data[DataName];
        else return null;
    }

    public void SetData(string DataName, string data)
    {
        if (Data.Count <= 0)
        {
            var SavedData = File.ReadAllText($"{Application.persistentDataPath}/UserData.txt");
            Data = JsonUtility.FromJson<Dictionary<string, string>>(SavedData);
        }

        if (Data.ContainsKey(DataName))
            Data[DataName] = data;
        else
            Data.Add(DataName, data);

        SaveFile();
    }

    public void SaveFile()
    {
        string sDirPath;
        sDirPath = Application.persistentDataPath;
        DirectoryInfo di = new DirectoryInfo(sDirPath);
        if (di.Exists == false)
        {
            di.Create();
        }

        var data = JsonUtility.ToJson(this);
        File.WriteAllText($"{Application.persistentDataPath}/UserData.txt", data);
        GameUtils.Log("Complete Save UserData");
    }

}
