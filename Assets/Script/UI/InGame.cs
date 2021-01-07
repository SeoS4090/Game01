using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGame : MonoBehaviour
{
    [SerializeField] RectTransform InGameRect;
    [SerializeField] Transform Pool;
    [SerializeField] GameObject Oritin_Tile;


    [SerializeField] TextMeshProUGUI Turn;

    [SerializeField] TextMeshProUGUI Changer;
    [SerializeField] TextMeshProUGUI Hammer;



    MSP_Game_Stage stage;
    bool Initalize = false;
    int[,] map;

    private void Start()
    {
        for(int index = InGameRect.childCount -1; index >= 0; index --)
        {
            var obj = InGameRect.GetChild(index);

            obj.SetParent(Pool);
            obj.gameObject.SetActive(false);
        }

        LoadMap(stage);
        Initalize = true;
    }

    public void Hide()
    {
        Core.Instance().Hide(this.name.Replace("(Clone)", ""));
    }
    public void Onclick_Close()
    {
        Core.Instance().ShowAndGet<UI_Popup_YesNo>("UI_Popup_YesNo")
            .SetView("게임을 종료하시겠습니까?")
            .SetBtn_Yes("Yes", Hide)
            .SetBtn_No("No");
    }
    public void SetMap(MSP_Game_Stage stage)
    {
        this.stage = stage;
        if (Initalize == true)
            LoadMap(stage);
    }

    public void LoadMap(MSP_Game_Stage stage)
    {
        map = new int[stage.Width, stage.Height];

        int index = 0;
        foreach(var _map in stage.Map)
        {
            int h = index / stage.Width;
            int w = index - (stage.Width * h);
            
            map[w, h] = _map;
            index++;
        }

        Turn.text = stage.ClearCount.ToString();

        Changer.text = Core.Instance().userDataMangaer.GetData_int("Item_ETC_Changer").ToString();
        Hammer.text = Core.Instance().userDataMangaer.GetData_int("Item_ETC_Hammer").ToString();

        #region 맵 생성


        for (int y = 0; y < stage.Height; y++)
        {
            for (int x = 0; x < stage.Width; x++)
            {
                var obj = GetorAdd($"Tile_{x}_{y}", InGameRect);

                obj.gameObject.GetOrAddComponent<RectTransform>().sizeDelta = new Vector2(64, 64);
                obj.gameObject.GetOrAddComponent<RectTransform>().anchoredPosition = new Vector3(64 * (x - stage.Width/2), 64 * (stage.Height/ 2 - y));



            }
        }

        #endregion

    }

    public Transform GetorAdd(string Name = "",Transform parent = null)
    {
        GameObject obj;

        if (Pool.childCount > 0)
        {
            obj = Pool.GetChild(0).gameObject;
        }
        else
            obj = GameObject.Instantiate(Oritin_Tile);

        if(Name != "")
            obj.name = Name;
        obj.transform.SetParent(parent);

        obj.gameObject.SetActive(true);
        return obj.transform;
    }



}
