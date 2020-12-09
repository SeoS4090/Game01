using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Core : MonoBehaviour
{
    static Core pthis = null;

    public static Core Instance()
    {
        if (pthis == null)
            pthis = new Core();

        return pthis;
    }


    private void Awake()
    {
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(PopupPool);
        pthis = this;
    }

}
