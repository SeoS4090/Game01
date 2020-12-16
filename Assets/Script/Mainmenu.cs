using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mainmenu : MonoBehaviour
{
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

}
