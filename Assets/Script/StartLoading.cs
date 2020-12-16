using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartLoading : MonoBehaviour
{
    [SerializeField] Slider ProgressBar;
    [SerializeField] TextMeshProUGUI ProgressText;

    Dictionary<string,Action> LoadingAction = new Dictionary<string, Action>();
    IDisposable disposable;


    void Awake()
    {
        ProgressBar.value = 0;
        ProgressText.text = "Loading";

        LoadingAction.Add("LoadDatabase", LoadDatabase);
        LoadingAction.Add("LoadMainMenu",LoadMainMenu);
    }


    void Start()
    {
        ProgressBar.maxValue = LoadingAction.Count;
        foreach (var action in LoadingAction)
        {
            ProgressBar.value++;
            ProgressText.text = $"{action.Key} ({ProgressBar.value}/{LoadingAction.Count})";
            
            action.Value.Invoke();
        }
    }

    void LoadMainMenu()
    {
        var Prefab = Resources.Load("MainMenu") as GameObject;
        var obj = GameObject.Instantiate(Prefab, this.transform.parent);
        Core.Instance().mainmenu = obj.GetComponent<Mainmenu>();
    }

    void LoadDatabase()
    {
        Core.Instance().dataManager.ReadAllDataBase();


    }
}
