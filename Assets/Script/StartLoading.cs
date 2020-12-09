using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class StartLoading : MonoBehaviour
{
    [SerializeField] Slider ProgressBar;
    [SerializeField] TextMeshProUGUI ProgressText;

    List<Action> LoadingAction;
    IDisposable disposable;


    void Awake()
    {
        LoadingAction = new List<Action>();
        ProgressBar.value = 0;
        ProgressText.text = "Loading";


        LoadingAction.Add(SliderChangeFill);
        LoadingAction.Add(LoadMainMenu);
    }


    void Start()
    {
        foreach(var action in LoadingAction)
        {
            action.Invoke();
            ProgressBar.value++;
            ProgressText.text = $"{ProgressBar.value}/{LoadingAction.Count}";
        }
    }


    void SliderChangeFill()
    {
        disposable?.Dispose();
        var color = ProgressBar.fillRect.GetComponent<Image>().color;
        color.b += 0.1f;
        disposable = Observable.Interval(TimeSpan.FromSeconds(3)).Subscribe(_=> 
        {
            color.b += 0.1f;
            ProgressBar.fillRect.GetComponent<Image>().color = color;
        });
    }

    void LoadMainMenu()
    {
        var Prefab = Resources.Load("Mainmenu") as GameObject;
        GameObject.Instantiate(Prefab, this.transform.parent);
    }
}
