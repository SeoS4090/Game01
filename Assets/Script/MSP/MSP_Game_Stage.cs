using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

[MessagePackObject]
public class MSP_Game_Stage
{

    [Key(0)] public string DataKey;
    [Key(1)] public string Load_Game_Data;
    [Key(2)] public List<int> Scores;
    [Key(3)] public List<string> GameType_Name;
    [Key(4)] public List<int> GameType_Count;

    public MSP_Game_Stage() { }

    public MSP_Game_Stage(KeyValuePair<string, Dictionary<string, object>> Data)
    {
        DataKey = Data.Key;
        Load_Game_Data = Convert.ToString(Data.Value["Load_Game_Data"]);
        Scores = JsonConvert.DeserializeObject<List<int>>(Convert.ToString(Data.Value["Scores"])); 
        GameType_Name = JsonConvert.DeserializeObject<List<string>>(Convert.ToString(Data.Value["GameType_Name"]));
        GameType_Count = JsonConvert.DeserializeObject<List<int>>(Convert.ToString(Data.Value["GameType_Count"]));
    }

}