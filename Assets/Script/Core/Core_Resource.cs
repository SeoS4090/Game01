using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Core : MonoBehaviour
{
    public Sprite GetIcon(string icon)
    {
        return Resources.Load<Sprite>($"Icons/{icon}");
    }


}
