using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGame_Block : MonoBehaviour
{
    [SerializeField] List<Sprite> NormalBlock = new List<Sprite>();
    public Image Img_Block { 
        get
            {
                return GetComponent<Image>();
            }
        set { } }
    
    public void SetNormalBlock()
    {
        #region 
        Img_Block.sprite = GetNormalBlock();

        #endregion
    }

    public Vector2 GetPos()
    {
        Vector2 vec = GetComponent<RectTransform>().anchoredPosition;
        vec.x = (int) vec.x / 64 ;
        vec.y = (int) vec.y / 64;

        return vec;
    }


    public Sprite GetNormalBlock()
    {
        int index = Random.Range(0, NormalBlock.Count);

        return NormalBlock[index];
    }

    public void OnClick_Block()
    {
        Core.Instance().ingame.Select_BlockAsync(Img_Block);
    }

    public int GetIndex()
    {
        foreach (var img in NormalBlock)
        {
            if(img == Img_Block.sprite)
            {
                return NormalBlock.IndexOf(img);
            }

        }

        GameUtils.Log($"[Error] Block index {GetPos()}");
        return -1;
    }

}
