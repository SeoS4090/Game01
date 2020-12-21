using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdsManager : MonoBehaviour
{
    Action CompleteAction = null;
    private void Start()
    {
        if(Advertisement.isInitialized == false)
        {

            string Gameid = Define.str_GooglePlayStore_Game_ID;
            if(Application.platform == RuntimePlatform.IPhonePlayer)
            {
                Gameid = Define.str_AppleStore_Game_ID;
            }

            Advertisement.Initialize(Gameid, Define.b_TestMode);
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                {
                    Debug.Log("The ad was successfully shown.");
                    // 여기에 보상 처리 
                    CompleteAction?.Invoke();
                }
                break;
            case ShowResult.Skipped:
                {
                    Debug.Log("The ad was skipped before reaching the end.");
                }

                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
        }

        CompleteAction = null;
    }

    public void ShowRewarded(Action Complete_Action)
    {
        if(CompleteAction != null)
        {
            GameUtils.LogError("광고 보상 진행중");
            return;
        }

        CompleteAction = Complete_Action;

        if (Advertisement.IsReady())
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);
        }
        else
        {
            Debug.Log("AD FAIL");
        }
    }
}
