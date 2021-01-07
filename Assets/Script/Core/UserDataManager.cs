using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json;

public class UserDataManager : MonoBehaviour
{
    Dictionary<string, string> Data = new Dictionary<string, string>();

    public string GetDailyData(string DataName)
    {
        var data = GetData(DataName);
        var setDate = GetData_long($"{DataName}_SetDate");

        if (new DateTime(setDate).Day < DateTime.Now.Day)
            data = "";

        return data;
    }

    public void SetDailyData(string DataName, string data)
    {
        SetData(DataName, data);
        SetData($"{DataName}_SetDate", DateTime.Now.Ticks.ToString());
    }

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
            SetData(DataName, defaultValue.ToString());
            return defaultValue;
        }

        return Convert.ToInt32(data);
    }

    public long GetData_long(string DataName, int defaultValue = 0)
    {
        var data = GetData(DataName);
        if (data == "")
        {
            SetData(DataName, defaultValue.ToString());
            return defaultValue;
        }

        return Convert.ToInt64(data);
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


        if(Define.Update_Mainmenu_list.Contains(DataName) == true)
        {
            Core.Instance().mainmenu.Refresh_Top();
        }


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
