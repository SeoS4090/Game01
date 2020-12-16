using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public partial class UI_Popup_Shop : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Text_Title;
    [SerializeField] RectTransform Content;

    #region Shop Data

    public enum ShopKinds
    {                   // ShopKinds
        Shop_Gold = 0,   
        Shop_Diamond = 1,
    }

    List<MSP_Shop> ShopData = new List<MSP_Shop>();

    #endregion


    #region 기본

    private void Awake()
    {
        Text_Title.text = "";
    }

    public void hide()
    {
        Core.Instance().Hide(this.name.Replace("(Clone)",""));
    }

    #endregion

    public UI_Popup_Shop SetView(ShopKinds kinds)
    {
        ShopData.Clear();

        foreach (var shopData in Core.Instance().dataManager.Shop)
        {
            if(shopData.Value.Shop_Kinds.Equals((int) kinds))
            {
                ShopData.Add(shopData.Value);
            }
        }


        Refresh();

        return this;
    }

    public void Refresh()
    {
        Clear();

        foreach(var data in ShopData)
        {
            var obj = GetorAdd();
            obj.SetData(data);
        }
    }

    public void Clear()
    {
        for (int i = 0; i < Content.childCount; i++)
        {
            Content.GetChild(i).gameObject.SetActive(false);
        }
    }

    public Item_Shop GetorAdd()
    {
        Item_Shop item = null;

        for (int i = 0; i < Content.childCount; i++)
        {
            if (Content.GetChild(i).gameObject.activeSelf == false)
            {
                item = Content.GetChild(i).GetComponent<Item_Shop>();
                item.gameObject.SetActive(true);
                break;
            }
        }

        if(item == null)
        {
            item = GameObject.Instantiate(Resources.Load<Item_Shop>("Popup/Popup_Item/Item_Shop"), Content);
        }
        
        return item;
    }

}
