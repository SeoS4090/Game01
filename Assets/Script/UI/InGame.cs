using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using System.Collections;

public class InGame : MonoBehaviour
{
    [SerializeField] RectTransform InGameBackGroundRect;
    [SerializeField] GameObject BackGroundTile; 

    [SerializeField] RectTransform InGameRect;
    [SerializeField] Transform Pool;
    [SerializeField] GameObject Origin_Tile;


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

        InGameRect.GetComponent<RectTransform>().sizeDelta = new Vector2(stage.Width * 64 , stage.Height * 64);


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
        for (int i = InGameBackGroundRect.childCount - 1; i >= 0; i--)
        {
            InGameBackGroundRect.GetChild(i).SetParent(Pool);
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
        selectedBlock = null;

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

        #region BG 맵 생성


        for (int y = 0; y < stage.Height; y++)
        {
            for (int x = 0; x < stage.Width; x++)
            {
                var obj = GetorAdd(BackGroundTile,$"BG_Tile_{x}_{y}", InGameBackGroundRect);

                obj.gameObject.GetOrAddComponent<RectTransform>().sizeDelta = new Vector2(64, 64);
                obj.gameObject.GetOrAddComponent<RectTransform>().anchoredPosition = new Vector3(64 * (x - stage.Width / 2), 64 * (stage.Height / 2 - y));
                //obj.gameObject.GetComponent<InGame_Block>().SetNormalBlock(x, y);
                //blocks.Add(obj.gameObject.GetComponent<InGame_Block>());
            }
        }

        #endregion

        #region 인게임 타일 생성

        for (int y = 0; y < stage.Height; y++)
        {
            for (int x = 0; x < stage.Width; x++)
            {
                var obj = GetorAdd(Origin_Tile, $"Tile_{new Vector2(64 * (x - stage.Width / 2), 64 * (stage.Height / 2 - y))}", InGameRect);

                obj.gameObject.GetOrAddComponent<RectTransform>().sizeDelta = new Vector2(64, 64);
                obj.gameObject.GetOrAddComponent<RectTransform>().anchoredPosition = new Vector3(64 * (x - stage.Width / 2), 64 * (stage.Height / 2 - y));
                obj.gameObject.GetComponent<InGame_Block>().SetNormalBlock();
                obj.name = $"Tile_{obj.GetComponent<InGame_Block>().GetPos()}";

                blocks.Add(obj.gameObject.GetComponent<InGame_Block>());
            }
        }

        #endregion

        CheckMap();
    }

    public void CheckMap()
    {
        bool bcheckmap = false;
        do
        {
            bcheckmap = false;
            foreach (var block in blocks)
            {
                #region 가로 체크

                InGame_Block[] horizon_arr = new InGame_Block[stage.Width];

                foreach (var tile in blocks)
                {
                    if (tile.GetPos().y != block.GetPos().y)
                        continue;

                    int index = (int)tile.GetPos().x + (int)(stage.Width / 2);
                    horizon_arr[index] = tile;
                }


                List<InGame_Block> H_clearBlock = new List<InGame_Block>();
                for (int i = 0; i < stage.Width; i++)
                {

                    if (horizon_arr[i].GetIndex() != block.GetIndex())
                    {
                        if (H_clearBlock.Count < 3)
                            H_clearBlock.Clear();

                        continue;
                    }

                    H_clearBlock.Add(horizon_arr[i]);
                }

                if (H_clearBlock.Count < 3)
                    H_clearBlock.Clear();
                else
                {

                    GameUtils.Log("가로 찾음");
                    block.SetNormalBlock();
                    bcheckmap = true;
                }


                #endregion

                #region 세로 체크

                InGame_Block[] vertical_arr = new InGame_Block[stage.Height];

                foreach (var tile in blocks)
                {
                    if (tile.GetPos().x != block.GetPos().x)
                        continue;

                    int index = (int)tile.GetPos().y + (int)(stage.Height / 2);
                    vertical_arr[index] = tile;
                }


                List<InGame_Block> V_clearBlock = new List<InGame_Block>();
                for (int i = 0; i < stage.Height; i++)
                {

                    if (vertical_arr[i].GetIndex() != block.GetIndex())
                    {
                        if (V_clearBlock.Count < 3)
                            V_clearBlock.Clear();

                        continue;
                    }

                    V_clearBlock.Add(vertical_arr[i]);
                }

                if (V_clearBlock.Count < 3)
                    V_clearBlock.Clear();

                else
                {

                    GameUtils.Log("세로 찾음");
                    block.SetNormalBlock();
                    bcheckmap = true;
                }

                #endregion

            }

        } while (bcheckmap) ;

    }

    public Transform GetorAdd(GameObject origin, string Name = "", Transform parent = null)
    {
        GameObject obj;

        var temp = Pool.Find(Name);
        if (temp == null)
        {
            obj = GameObject.Instantiate(origin);
        }
        else
        {
            obj = temp.gameObject;
            GameUtils.Log($"Recycle {Name}");
        }

        if (Name != "")
            obj.name = Name;


        obj.transform.SetParent(parent);

        obj.gameObject.SetActive(true);
        
        return obj.transform;
    }



    Image selectedBlock;

    public async Task Select_BlockAsync(Image block)
    {
        GameUtils.Log($"{block.GetComponent<InGame_Block>().GetPos()} is selected");

        //취소
        if (block == selectedBlock)
        {
            selectedBlock = null;

            return;
        }

        //변경
        if (selectedBlock != null)
        {
            var Clicked_Block_pos = block.GetComponent<InGame_Block>().GetPos();
            var Selected_Block_pos = selectedBlock.GetComponent<InGame_Block>().GetPos();

            bool canChange = Mathf.Abs(Clicked_Block_pos.x - Selected_Block_pos.x) <= 0 || Mathf.Abs(Clicked_Block_pos.y - Selected_Block_pos.y) <= 0;


            if (canChange)
            {
                InGame_Block block_1 = selectedBlock.GetComponent<InGame_Block>();
                InGame_Block block_2 = block.GetComponent<InGame_Block>();

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
        GameUtils.Log("drop down");

        for (int i = blocks.Count - 1; i >= 0; i--)
        {
            if (blocks[i].gameObject.activeSelf == true)
                continue;

            List<InGame_Block> vertical = new List<InGame_Block>();

            //vertical.Add(blocks[i]);

            foreach (var block in blocks)
            {
                if (block.GetPos().x.Equals(blocks[i].GetPos().x) && blocks[i].GetPos().y < block.GetPos().y)
                {
                    vertical.Add(block);
                }
            }

            blocks[i].transform.localPosition = new Vector2(blocks[i].transform.localPosition.x                                                                                                                                                                                                                , (stage.Height + 1) * 32);
            blocks[i].SetNormalBlock();


            foreach (var down in vertical)
            {
                //down.GetComponent<Image>().color = Color.gray;
                down.gameObject.SetActive(true);

                down.name = $"Tile_{down.GetComponent<InGame_Block>().GetPos() + Vector2.down}";


                down.transform.DOMove(down.transform.position + Vector3.down * 64, 0.2f, true);
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
        
        InGame_Block[] horizon_arr = new InGame_Block[stage.Width];
        
        foreach (var tile in blocks)
        {
            if (tile.GetPos().y != block.GetPos().y)
                continue;

            int index = (int)tile.GetPos().x + (int) (stage.Width / 2);
            horizon_arr[index] = tile;
        }


        List<InGame_Block> H_clearBlock = new List<InGame_Block>();
        for(int i = 0; i < stage.Width; i++)
        {

            if (horizon_arr[i].GetIndex() != block.GetIndex())
            {
                if (H_clearBlock.Count < 3)
                    H_clearBlock.Clear();

                continue;
            }

            H_clearBlock.Add(horizon_arr[i]);
        }

        if (H_clearBlock.Count < 3)
            H_clearBlock.Clear();

        else
        {
            GameUtils.Log($"[{block.GetPos()}] 가로 완성 ({H_clearBlock.Count })개 / {block.Img_Block.sprite.name}");

            foreach (var img in H_clearBlock)
            {
                StartCoroutine(shake(img));
            }

            result = true;
        }



        #endregion

        #region 세로 체크

        InGame_Block[] vertical_arr = new InGame_Block[stage.Height];

        foreach (var tile in blocks)
        {
            if (tile.GetPos().x != block.GetPos().x)
                continue;

            int index = (int)tile.GetPos().y + (int)(stage.Height/ 2);
            vertical_arr[index] = tile;
        }


        List<InGame_Block> V_clearBlock = new List<InGame_Block>();
        for (int i = 0; i < stage.Height; i++)
        {

            if (vertical_arr[i].GetIndex() != block.GetIndex())
            {
                if (V_clearBlock.Count < 3)
                    V_clearBlock.Clear();

                continue;
            }

            V_clearBlock.Add(vertical_arr[i]);
        }

        if (V_clearBlock.Count < 3)
            V_clearBlock.Clear();

        else
        {
            GameUtils.Log($"[{block.GetPos()}] 세로 완성 ({V_clearBlock.Count })개 / {block.Img_Block.sprite.name}");

            foreach (var img in V_clearBlock)
            {
                StartCoroutine(shake(img));
            }

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
        block.gameObject.SetActive(false);
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

    }

    public IEnumerator Block_Swap(InGame_Block from, InGame_Block to,bool notchange = false,float duration = 0.2f)
    {
        var time = System.DateTime.Now;

        from.transform.DOMove(to.Img_Block.transform.position, duration);
        to.transform.DOMove(from.Img_Block.transform.position, duration);

        var nameTemp = from.name;
        from.name = to.name;
        to.name = nameTemp;

        //to.GetComponent<Button>().targetGraphic = to.Img_Block;
        //from.GetComponent<Button>().targetGraphic = from.Img_Block;

        yield return new WaitForSeconds(duration);

        if (notchange)
            yield break;

        GameUtils.Log(new System.TimeSpan(System.DateTime.Now.Ticks - time.Ticks).TotalSeconds.ToString());
        
        StartCoroutine(Check_BlockAsync(from, to));
    }
}
