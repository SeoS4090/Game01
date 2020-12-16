using MessagePack;
using System;
using System.Collections.Generic;

[MessagePackObject]
public class MSP_Localization
{
    [Key(0)] public string DataKey;
    [Key(1)] public string Korean;
    [Key(2)] public string English;


    public MSP_Localization() { }

    public MSP_Localization(KeyValuePair<string, Dictionary<string, object>> Data)
    {
        DataKey = Data.Key;
        Korean = Convert.ToString(Data.Value["Korean"]);
        English = Convert.ToString(Data.Value["English"]);
    }

}