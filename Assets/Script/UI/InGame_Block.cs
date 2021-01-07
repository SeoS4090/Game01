using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGame_Block : MonoBehaviour
{
    [SerializeField] List<Sprite> NormalBlock = new List<Sprite>();


    public void SetNormalBlock()
    {
        #region 
    }



    public Sprite GetNormalBlock()
    {
        return NormalBlock[Random.Range(0, NormalBlock.Count)];
    }
}
