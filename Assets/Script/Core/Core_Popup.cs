using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Core : MonoBehaviour
{
    Canvas PopupPool;

    Dictionary<string, GameObject> Pool = new Dictionary<string, GameObject>();

    public void ShowPopup(string popupName)
    {
        if(Pool.ContainsKey(popupName))
        {
            Pool[popupName].SetActive(true);
            return;
        }


        var prefab = Resources.Load($"Popup/{popupName}") as GameObject;
        var obj = GameObject.Instantiate(prefab, PopupPool.transform);
        Pool.Add(popupName, obj);
    }

    public T ShowAndGet<T>(string popupName)
    {
        ShowPopup(popupName);
        return Pool[popupName].GetComponent<T>();
    }

    public void Hide(string Popupname)
    {
        Pool[Popupname].SetActive(false);
    }
}
