using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGame_Block : MonoBehaviour
{
    [SerializeField] List<Sprite> NormalBlock = new List<Sprite>();
    public Image Img_Block;
    Vector2 pos;
    

    public void SetNormalBlock(int x , int y)
    {
        #region 
        Img_Block.sprite = GetNormalBlock();

        pos = new Vector2(x, y);

        #endregion
    }

    public Vector2 GetPos()
    {
        return pos;
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

        GameUtils.Log($"[Error] Block index {pos}");
        return -1;
    }

}
