using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public partial class UI_Popup_Shop : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Text_Title;

    #region Shop Data

    public enum ShopKinds
    {
        Shop_Diamond,
        Shop_Gold,
    }

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
        Show_DiamondShop();


        return this;
    }

    public void Show_DiamondShop()
    {

    }

}
