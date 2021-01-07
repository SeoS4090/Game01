using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

[MessagePackObject]
public class MSP_Game_Stage
{
    [Key(0)] public string DataKey;
    [Key(1)] public List<int> Scores;
    [Key(2)] public List<string> GameType_Name;
    [Key(3)] public List<int> GameType_Count;
    [Key(4)] public int Width;
    [Key(5)] public int Height;
    [Key(6)] public List<int> Map;
    [Key(7)] public int ClearCount;

    public MSP_Game_Stage() { }

    public MSP_Game_Stage(KeyValuePair<string, Dictionary<string, object>> Data)
    {
        DataKey = Data.Key;
        Scores = JsonConvert.DeserializeObject<List<int>>(Convert.ToString(Data.Value["Scores"]));
        GameType_Name = JsonConvert.DeserializeObject<List<string>>(Convert.ToString(Data.Value["GameType_Name"]));
        GameType_Count = JsonConvert.DeserializeObject<List<int>>(Convert.ToString(Data.Value["GameType_Count"]));

        Width = Convert.ToInt32(Data.Value["Width"]);
        Height = Convert.ToInt32(Data.Value["Height"]);
        ClearCount = Convert.ToInt32(Data.Value["ClearCount"]);
        Map = JsonConvert.DeserializeObject<List<int>>(Convert.ToString(Data.Value["Map"]));
    }

}