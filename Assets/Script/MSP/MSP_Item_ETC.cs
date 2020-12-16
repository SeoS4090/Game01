using MessagePack;
using System;
using System.Collections.Generic;

[MessagePackObject]
public class MSP_Item_ETC
{
    [Key(0)] public string DataKey;
    [Key(1)] public string Name;
    [Key(2)] public string Image;


    public MSP_Item_ETC() { }

    public MSP_Item_ETC(KeyValuePair<string, Dictionary<string, object>> Data)
    {
        DataKey = Data.Key;
        Image = Convert.ToString(Data.Value["Image"]);
        Name = Convert.ToString(Data.Value["Name"]);
    }

}