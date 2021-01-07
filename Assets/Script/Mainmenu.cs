using MessagePack;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;

public class Mainmenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Text_UserName;
    [SerializeField] TextMeshProUGUI Text_Gold;
    [SerializeField] TextMeshProUGUI Text_Diamond;

    private void Start()
    {
        Refresh_Top();
    }

    public void Refresh_Top()
    {
        Text_UserName.text = Core.Instance().userDataMangaer.GetData("User_Nick_Name");
        Text_Gold.text = Core.Instance().userDataMangaer.GetData_int("Item_ETC_Gold").ToString();
        Text_Diamond.text = Core.Instance().userDataMangaer.GetData_int("Item_ETC_Diamond").ToString();

        CheckNickName();
    }

    public void CheckNickName()
    {
        if(Text_UserName.text.Equals(""))
        {
            Core.Instance().ShowPopup("UI_Popup_SetNickName");
        }
    }



    public void OnClick_Menu()
    {
        Core.Instance().ShowPopup("UI_Popup_YesNo");
    }

    public void Onclick_Diamond_Shop()
    {
        var shop = Core.Instance().ShowAndGet<UI_Popup_Shop>("UI_Popup_Shop");
        shop.SetView(UI_Popup_Shop.ShopKinds.Shop_Diamond);
    }

    public void Onclick_Gold_Shop()
    {
        var shop = Core.Instance().ShowAndGet<UI_Popup_Shop>("UI_Popup_Shop");
        shop.SetView(UI_Popup_Shop.ShopKinds.Shop_Gold);
    }

    public void StartGame(string stage)
    {
        string path = $"{Application.dataPath}/Resources/DataBase/MapData/{stage}.bytes";
        var bytes = File.ReadAllBytes(path);
        var StageData = MessagePackSerializer.Deserialize<MSP_Game_Stage>(bytes);

        Core.Instance().ShowAndGet<InGame>("InGame").SetMap(StageData);

    }

    public void Onclick_RecentGameStage()
    {
        var recentStage = Core.Instance().userDataMangaer.GetData_int("RecentStage",1);
        StartGame($"Map_Stage_{recentStage}");
    }

}
