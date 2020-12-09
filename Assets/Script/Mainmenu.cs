using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mainmenu : MonoBehaviour
{
    public void OnClick_Menu()
    {
        Core.Instance().ShowPopup("UI_Popup_YesNo");
    }
}
