using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using MessagePack;
using MessagePack.Resolvers;

public class DataBaseManager : MonoBehaviour
{
    #region
    public Dictionary<string, MSP_Game_Base> GameBase;
    public Dictionary<string, MSP_Shop> Shop;
    public Dictionary<string, MSP_Item_ETC> Item_ETC;
    public Dictionary<string, MSP_Localization> Localization;

    #endregion

    bool serializerRegistered = false;
    private void Awake()
    {
        if (!serializerRegistered)
        {
            StaticCompositeResolver.Instance.Register(
                 MessagePack.Resolvers.GeneratedResolver.Instance,
                 MessagePack.Resolvers.StandardResolver.Instance
            );

            var option = MessagePackSerializerOptions.Standard.WithResolver(StaticCompositeResolver.Instance);

            MessagePackSerializer.DefaultOptions = option;
            serializerRegistered = true;
        }
    }

    public void ReadAllDataBase()
    {
        string path = $"{Application.dataPath}/Resources/DataBase";

        DirectoryInfo dir = new DirectoryInfo(path);

        if (dir.Exists == false)
        {
            GameUtils.LogError("폴더 없음");
            return;
        }

        List<string> DatabaseIndex = new List<string>();

        foreach (var database in dir.GetFiles())
        {
            if (database.Name.EndsWith(".meta"))
                continue;

            var bytes = File.ReadAllBytes(database.FullName);


            #region Deserialize
            if (database.Name.Replace(".bytes", "").Equals("Game_Base"))
            {
                GameBase = MessagePack.MessagePackSerializer.Deserialize<Dictionary<string, MSP_Game_Base>>(bytes);
            }

            if (database.Name.Replace(".bytes", "").Equals("Shop"))
            {
                Shop = MessagePack.MessagePackSerializer.Deserialize<Dictionary<string, MSP_Shop>>(bytes);
            }

            if (database.Name.Replace(".bytes", "").Equals("Item_ETC"))
            {
                Item_ETC = MessagePack.MessagePackSerializer.Deserialize<Dictionary<string, MSP_Item_ETC>>(bytes);
            }
            if (database.Name.Replace(".bytes", "").Equals("Localization"))
            {
                Localization = MessagePack.MessagePackSerializer.Deserialize<Dictionary<string, MSP_Localization>>(bytes);
            }



            #endregion



        }
    }

}
