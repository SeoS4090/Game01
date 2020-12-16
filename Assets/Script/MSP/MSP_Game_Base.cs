using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

[MessagePackObject]
public class MSP_Game_Base
{
    [Key(0)] public string DataKey;
    [Key(1)] public int Var_int;
    [Key(2)] public float Var_float;
    [Key(3)] public string Var_string;
    [Key(4)] public List<int> Var_list_int;
    [Key(5)] public List<float> Var_list_float;
    [Key(6)] public List<string> Var_list_string;

    public MSP_Game_Base() { }
    public MSP_Game_Base(KeyValuePair<string, Dictionary<string, object>> Data)
    {
        DataKey = Data.Key;

        Var_int = Convert.ToInt32(Data.Value["Var_int"]);
        Var_float = Convert.ToSingle(Data.Value["Var_float"]);
        Var_string = Convert.ToString(Data.Value["Var_string"]);

        Var_list_int = JsonConvert.DeserializeObject<List<int>>(Convert.ToString(Data.Value["Var_list_int"]));
        Var_list_float = JsonConvert.DeserializeObject<List<float>>(Convert.ToString(Data.Value["Var_list_float"]));
        Var_list_string = JsonConvert.DeserializeObject<List<string>>(Convert.ToString(Data.Value["Var_list_string"]));

    }

}