using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item_Shop : MonoBehaviour
{
    [SerializeField] Image Icon;
    [SerializeField] Image Buy_image;
    [SerializeField] TextMeshProUGUI Buy_Amount;

    string itemName;
    MSP_Shop Data;
    public void Click_Buy()
    {
        var purchase_Text = "";

        if(Core.Instance().dataManager.Item_ETC.ContainsKey(Data.Goods))
        {
            purchase_Text = $"{Data.Name} 을 구매 하시겠습니까?\n필요 재화 : {Core.Instance().dataManager.Item_ETC[Data.Goods].Name}";
        }

        if (Core.Instance().dataManager.GameBase.ContainsKey(Data.Goods))
        {
            purchase_Text = $"{Data.Name} 을 광고 보고 얻으시겠습니까 ?";
        }



        Core.Instance().ShowAndGet<UI_Popup_YesNo>("UI_Popup_YesNo")
            .SetView(purchase_Text)
            .SetBtn_No("취소")
            .SetBtn_Yes("구매", ()=> {

                Buy();
                

            });
    }

    public bool CanShowAds(string AdsKey)
    {
        //광고 틱 계산
        if (Core.Instance().dataManager.GameBase.ContainsKey(Data.Goods))
        {

            var daily_data = Core.Instance().userDataMangaer.GetDailyData(Data.Goods);
            if (daily_data == "")
                daily_data = "0";

            var Ads_tick = Convert.ToInt32(daily_data);
            var adsData = Core.Instance().dataManager.GameBase[Data.Goods];


            if(Ads_tick < adsData.Var_int)
            {
                Ads_tick++;
                Core.Instance().userDataMangaer.SetDailyData(Data.Goods, Ads_tick.ToString());
                return true;
            }

            return false;
        }
        return false;
    }

    public void Buy()
    {

        //광고 구매
        if(Data.GoodsAmount < 0)
        {
            if(CanShowAds(Data.Goods))
            {
                Core.Instance().adsManager.ShowRewarded(() => {

                    var CurrentData = Core.Instance().userDataMangaer.GetData_int(Data.Reward_Item);
                    Core.Instance().userDataMangaer.SetData(Data.Reward_Item, $"{CurrentData + Data.Reward_Count}");
                });
            }
        }
        

        // 재화 사용해서 구매
        var canBuy = Core.Instance().userDataMangaer.GetData_int(Data.Goods) > Data.GoodsAmount;
        if(!canBuy)
        {
            Core.Instance().ShowAndGet<UI_Popup_YesNo>("UI_Popup_YesNo")
                .SetView("재화가 부족 합니다.")
                .SetBtn_Yes("확인");
        }

        else
        {
            Core.Instance().ShowAndGet<UI_Popup_YesNo>("UI_Popup_YesNo")
                .SetView("구매가 완료 되었습니다.")
                .SetBtn_Yes("확인");
        }
    }

    public void SetData(MSP_Shop data)
    {
        Data = data;

        Icon.sprite = Core.Instance().GetIcon($"Image_Shop/{data.Icon}");

        if(Core.Instance().dataManager.Item_ETC.ContainsKey(data.Goods))
        {
            var ProductItem = Core.Instance().dataManager.Item_ETC[data.Goods];
            Buy_image.sprite = Core.Instance().GetIcon(ProductItem.Image);
        }



        if (data.GoodsAmount > 0)
            Buy_Amount.text = data.GoodsAmount.ToString();

        else if (Core.Instance().dataManager.GameBase.ContainsKey(data.Goods) == true) // 광고
            Buy_Amount.text = "광고 보기";

        else
            Buy_Amount.text = "InAPP"; // 인앱

    }

}
