using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Popup_SetNickName : MonoBehaviour
{
    [SerializeField] TMP_InputField inputfield;


   public void Hide()
    {
        Core.Instance().Hide(this.name.Replace("(Clone)", ""));
    }

    public void Onclick_Check()
    {
        if (inputfield.text.Trim().Length > 0 && inputfield.text.Trim().Length < 10)
        {
            Core.Instance().userDataMangaer.SetData("User_Nick_Name", inputfield.text.Trim());
            Core.Instance().mainmenu.Refresh_Top();
            Hide();
            
        }
        else
        {
            var popup = Core.Instance().ShowAndGet<UI_Popup_YesNo>("UI_Popup_YesNo").SetView("Description_Set_NickName");
            popup.SetBtn_Yes("Confirm", ()=> { popup.hide(); });
        }
    }


}
