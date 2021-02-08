using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Core : MonoBehaviour
{
    static Core pthis = null;
    public UserDataManager userDataMangaer;
    public DataBaseManager dataManager;
    public AdsManager adsManager;

    public InGame ingame;
    [HideInInspector] public Mainmenu mainmenu;

    public static Core Instance()
    {
        if (pthis == null)
            pthis = new Core();

        return pthis;
    }


    private void Awake()
    {
        DontDestroyOnLoad(this);

        PopupPool = new GameObject("PopupPool").AddComponent<Canvas>();
        PopupPool.sortingOrder = 2;
        PopupPool.renderMode = RenderMode.ScreenSpaceOverlay;

        PopupPool.gameObject.AddComponent<GraphicRaycaster>();
        
        var Scaler = PopupPool.gameObject.AddComponent<CanvasScaler>();
        Scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        Scaler.referenceResolution = new Vector2(720, 1280);

        DontDestroyOnLoad(PopupPool);
        pthis = this;
    }



}
