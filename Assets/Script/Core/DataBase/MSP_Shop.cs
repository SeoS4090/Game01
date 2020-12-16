using MessagePack;
using System;
using System.Collections.Generic;

[MessagePackObject]
public class MSP_Shop
{
    [Key(0)] public string DataKey;
    [Key(1)] public string Goods;
    [Key(2)] public int GoodsAmount;
    [Key(3)] public int Shop_Kinds;
    [Key(4)] public string Icon;
    [Key(5)] public string Description;
    [Key(6)] public string Name;

    public MSP_Shop() { }

    public MSP_Shop(KeyValuePair<string, Dictionary<string, object>> Data)
    {
        DataKey = Data.Key;
        Goods = Convert.ToString(Data.Value["Goods"]);
        GoodsAmount = Convert.ToInt32(Data.Value["GoodsAmount"]);
        Shop_Kinds = Convert.ToInt32(Data.Value["Shop_Kinds"]);
        Icon = Convert.ToString(Data.Value["Icon"]);
        Description = Convert.ToString(Data.Value["Description"]);
        Name = Convert.ToString(Data.Value["Name"]);
    }

}