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
    void Click_Buy()
    {
        GameUtils.Log($"Buy {Data}");
    }
}
