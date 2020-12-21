using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json;

public class UserDataManager : MonoBehaviour
{
    Dictionary<string, string> Data = new Dictionary<string, string>();

    public string GetData(string DataName)
    {
        if(Data.Count <= 0)
        {
            if (File.Exists($"{Application.persistentDataPath}/UserData.txt"))
            {
                var SavedData = File.ReadAllText($"{Application.persistentDataPath}/UserData.txt");
                Data = JsonConvert.DeserializeObject<Dictionary<string, string>>(SavedData);
            }
            else
                Data = new Dictionary<string, string>();
            
        }

        if (DataName == null)
            DataName = "";
        if (Data.ContainsKey(DataName))
            return Data[DataName];
        else return "";
    }

    public int GetData_int(string DataName, int defaultValue = 0)
    {
        var data = GetData(DataName);
        if (data == "")
        {
            SetData(DataName, "0");
            return defaultValue;
        }

        return Convert.ToInt32(data);
    }


    public void SetData(string DataName, string data)
    {
        if (Data.Count <= 0)
        {
            if (File.Exists($"{Application.persistentDataPath}/UserData.txt"))
            {
                var SavedData = File.ReadAllText($"{Application.persistentDataPath}/UserData.txt");
                Data = JsonConvert.DeserializeObject<Dictionary<string, string>>(SavedData);
            }
            else
                Data = new Dictionary<string, string>();
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

        var data = JsonConvert.SerializeObject(Data);
        File.WriteAllText($"{Application.persistentDataPath}/UserData.txt", data);
        GameUtils.Log("Complete Save UserData");
    }

}
