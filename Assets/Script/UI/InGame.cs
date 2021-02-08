using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using System.Collections;

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

    List<InGame_Block> blocks = new List<InGame_Block>();

    private void Start()
    {
        for (int index = InGameRect.childCount - 1; index >= 0; index--)
        {
            var obj = InGameRect.GetChild(index);

            obj.SetParent(Pool);
            obj.gameObject.SetActive(false);
        }

        LoadMap(stage);
        Initalize = true;

        if (Core.Instance().ingame != null && Core.Instance().ingame != this)
        {
            Destroy(Core.Instance().ingame);
        }

        Core.Instance().ingame = this;
    }

    public void Hide()
    {
        for (int i = InGameRect.childCount - 1; i >= 0; i--)
        {
            InGameRect.GetChild(i).SetParent(Pool);
        }

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

        blocks.Clear();

        map = new int[stage.Width, stage.Height];

        int index = 0;
        foreach (var _map in stage.Map)
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
                obj.gameObject.GetOrAddComponent<RectTransform>().anchoredPosition = new Vector3(64 * (x - stage.Width / 2), 64 * (stage.Height / 2 - y));

                obj.gameObject.GetComponent<InGame_Block>().SetNormalBlock(x, y);

                blocks.Add(obj.gameObject.GetComponent<InGame_Block>());
            }
        }

        #endregion

    }

    public Transform GetorAdd(string Name = "", Transform parent = null)
    {
        GameObject obj;

        if (Pool.childCount > 0)
        {
            obj = Pool.GetChild(0).gameObject;
        }
        else
            obj = GameObject.Instantiate(Oritin_Tile);

        if (Name != "")
            obj.name = Name;
        obj.transform.SetParent(parent);

        obj.gameObject.SetActive(true);


        obj.GetComponent<InGame_Block>().Img_Block.color = new Color(1, 1, 1, 1);
        obj.GetComponent<InGame_Block>().Img_Block.transform.localPosition = Vector3.zero;
        return obj.transform;
    }



    Image selectedBlock;

    public async Task Select_BlockAsync(Image block)
    {
        GameUtils.Log($"{block.transform.parent.GetComponent<InGame_Block>().GetPos()} is selected");

        //취소
        if (block == selectedBlock)
        {
            selectedBlock = null;

            return;
        }

        //변경
        if (selectedBlock != null)
        {
            var Clicked_Block_pos = block.transform.parent.GetComponent<InGame_Block>().GetPos();
            var Selected_Block_pos = selectedBlock.transform.parent.GetComponent<InGame_Block>().GetPos();

            bool canChange = Mathf.Abs(Clicked_Block_pos.x - Selected_Block_pos.x) <= 0 || Mathf.Abs(Clicked_Block_pos.y - Selected_Block_pos.y) <= 0;


            if (canChange)
            {
                InGame_Block block_1 = selectedBlock.transform.parent.GetComponent<InGame_Block>();
                InGame_Block block_2 = block.transform.parent.GetComponent<InGame_Block>();

                StartCoroutine(Block_Swap(block_1, block_2));
                
                //Check_BlockAsync(block_1, block_2);


                selectedBlock = null;

                return;
            }

        }

        //선택
        selectedBlock = block;
    }

    public void Drop_Down()
    {
        for (int i = blocks.Count - 1; i >= 0; i--)
        {
            if (blocks[i].Img_Block.sprite != null)
                continue;

            InGame_Block up = null, left = null, right = null;

            for (int change_index = i - 1; change_index > 0; change_index--)
            {
                if (blocks[change_index].GetPos().y + 1 != blocks[i].GetPos().y)
                    continue;

                if (blocks[change_index].GetPos().x == blocks[i].GetPos().x)
                    up = blocks[change_index];

                if (blocks[change_index].GetPos().x + 1 == blocks[i].GetPos().x)
                    left = blocks[change_index];

                if (blocks[change_index].GetPos().x - 1 == blocks[i].GetPos().x)
                    right = blocks[change_index];
            }

            if (up != null)
            {
                continue;
            }

            if (left != null)
            {
                continue;
            }

            if (right != null)
            {
                continue;
            }
        }
    }


    public IEnumerator Check_BlockAsync(InGame_Block block_1, InGame_Block block_2)
    {
        bool second = Check_BlockAsync(block_2);
        bool first = Check_BlockAsync(block_1);

        if (first == false && second == false)
        {
            GameUtils.Log("Fail to made");
            StartCoroutine(Block_Swap(block_1, block_2, true));
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            Drop_Down();
        }
    }


    public bool Check_BlockAsync(InGame_Block block)
    {
        bool result = false;

        #region 가로 체크

        List<InGame_Block> horizon = new List<InGame_Block>();

        foreach (var tile in blocks)
        {
            if (tile.GetPos().y != block.GetPos().y)
                continue;

            //GameUtils.Log($"{ tile.Img_Block.sprite.name}");

            if (tile.GetIndex() != block.GetIndex() || block.GetIndex() < 0 )
            {
                if (horizon.Count < 3)
                    horizon.Clear();

                continue;
            }

            horizon.Add(tile);

        }

        if (horizon.Count < 3)
            horizon.Clear();

        else
        {
            GameUtils.Log($"[{block.GetPos()}] 가로 완성 ({horizon.Count })개 / {block.Img_Block.sprite.name}");

            foreach (var img in horizon)
            {
                StartCoroutine(shake(img));

            }

            result = true;
        }



        #endregion


        #region 세로 체크

        List<InGame_Block> vertical = new List<InGame_Block>();

        foreach (var tile in blocks)
        {
            if (tile.GetPos().x != block.GetPos().x)
                continue;

            //GameUtils.Log($"{ tile.Img_Block.sprite.name}");

            if (tile.GetIndex() != block.GetIndex())
            {
                if (vertical.Count < 3)
                    vertical.Clear();

                continue;
            }

            vertical.Add(tile);

        }

        if (vertical.Count < 3)
            vertical.Clear();

        else
        {
            GameUtils.Log($"[{block.GetPos()}] 세로 완성 ({vertical.Count })개 / {block.Img_Block.sprite.name}");
            result = true;
        }



        #endregion


        #region 사각형




        #endregion

        return result;
    }

    public IEnumerator shake(InGame_Block block, float sec = 0.5f)
    {
        block.Img_Block.transform.DOShakePosition(sec, 10);
        yield return new WaitForSeconds(sec);


        block.Img_Block.sprite = null;
        block.Img_Block.color = new Color(1, 1, 1, 0.0f);
        InGame_Block up = null, left = null, right = null;
        for (int i = 0; i < blocks.Count; i++)
        {
            if (block.GetPos().y + 1 != blocks[i].GetPos().y)
                continue;

            if (block.GetPos().x == blocks[i].GetPos().x)
                up = blocks[i];
            if (block.GetPos().x == blocks[i].GetPos().x + 1)
                left = blocks[i];
            if (block.GetPos().x == blocks[i].GetPos().x - 1)
                right = blocks[i];
        }

        if (up != null)
        {
            StartCoroutine(Block_Swap(block, up));
            GameUtils.Log("UP");
            yield break;
        }
        if (left != null)
        {
            GameUtils.Log("LEFT");
            yield break;
        }
        if (right != null)
        {
            GameUtils.Log("RIGHT");
            yield break;
        }
    }

    public IEnumerator Block_Swap(InGame_Block from, InGame_Block to,bool notchange = false,float duration = 0.2f)
    {

        var time = System.DateTime.Now;


        from.Img_Block.transform.DOMove(to.Img_Block.transform.position, duration);
        to.Img_Block.transform.DOMove(from.Img_Block.transform.position, duration);

        to.Img_Block.transform.SetParent(from.transform);
        from.Img_Block.transform.SetParent(to.transform);

        var temp = from.Img_Block;
        from.Img_Block = to.Img_Block;
        to.Img_Block = temp;

        to.GetComponent<Button>().targetGraphic = to.Img_Block;
        from.GetComponent<Button>().targetGraphic = from.Img_Block;


        yield return new WaitForSeconds(duration);

        if (notchange)
            yield break;

        GameUtils.Log(new System.TimeSpan(System.DateTime.Now.Ticks - time.Ticks).TotalSeconds.ToString());
        
        StartCoroutine(Check_BlockAsync(from, to));
    }
}
