using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UI_Popup_YesNo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Text_Title;
    [SerializeField] TextMeshProUGUI Text_Context;
    [SerializeField] TextMeshProUGUI Text_Yes;
    [SerializeField] TextMeshProUGUI Text_No;

    Action Action_yes;
    Action Action_No;

    #region 기본

    private void Awake()
    {
        Text_Context.text = "";
    }

    public void hide()
    {
        Core.Instance().Hide(this.name.Replace("(Clone)",""));
    }

    #endregion

    public UI_Popup_YesNo SetView(string Context, string title = "")
    {
        Text_Context.text = Context;
        Text_Title.text = title;
        
        Action_yes = null;
        Action_No = null;

        return this;
    }

    public UI_Popup_YesNo SetBtn_Yes(string Yes = "", Action action = null)
    {
        Text_Yes.transform.parent.gameObject.SetActive(true);
        Text_Yes.text = Yes;
        Action_yes = action;
        return this;
    }

    public UI_Popup_YesNo SetBtn_No(string No = "", Action action = null)
    {
        Text_No.transform.parent.gameObject.SetActive(true);
        Text_No.text = No;
        Action_No = action;
        return this;
    }

    public void Onclick_Yes()
    {
        Action_yes.Invoke();
    }

    public void Onclick_No()
    {
        Action_No.Invoke();
    }
}
