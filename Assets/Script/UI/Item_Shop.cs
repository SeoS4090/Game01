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
        GameUtils.Log($"Buy {Data}");
        Core.Instance().adsManager.ShowRewarded(() => {

            var CurrentData = Core.Instance().userDataMangaer.GetData_int(Data.Reward_Item);
            Core.Instance().userDataMangaer.SetData(Data.Reward_Item, $"{CurrentData + Data.Reward_Count}");
        });

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
        
        Buy_Amount.text = data.GoodsAmount.ToString();

    }

}
