using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//开始界面的UI管理
public class StartSceneMgr : MonoBehaviour
{
    public static StartSceneMgr Instance { get; private set; }

    private StartWind startWind;
    private LoadingWind loadingWind;
    private MapBgWind mapBgWind;

    private Transform Canvas;

    public void Init()
    {
        Instance = this;
        Canvas = transform.Find("Canvas");
        startWind = Canvas.Find("StartWind").GetComponent<StartWind>();
        loadingWind = Canvas.Find("LoadingWind").GetComponent<LoadingWind>();
        mapBgWind = Canvas.Find("MapBgWind").GetComponent<MapBgWind>();
        startWind.Init();
        loadingWind.Init();
        mapBgWind.Init();

        InitWind();

        Debug.Log("Init StartSceneMgr Done.");
    }
    void InitWind()
    {

        for (int i = 0; i < Canvas.childCount; i++)
        {
            Canvas.GetChild(i).gameObject.SetActive(false);
        }

        OpenStartWind();
        OpenMapBgWind();
    }

    public void EnterGameScene()
    {
        CloseMapBgWind();
        CloseStartWind();
        GameSceneMgr.Instance.EnterGameScene();
    }

    //MapWind
    public void OpenMapBgWind()
    {
        mapBgWind.SetWindState();
    }
    public void CloseMapBgWind()
    {
        mapBgWind.SetWindState(false);
    }
    public void RefreshUI_MapBg()
    {
        mapBgWind.RefreshUI();
    }

    //StartWind
    public void OpenStartWind()
    {
        startWind.SetWindState();
    }
    public void CloseStartWind()
    {
        startWind.SetWindState(false);
    }

    //LoadingWind
    public void OpenLoadingWind()
    {
        loadingWind.SetWindState();
    }
    public void CloseLoadingWind()
    {
        loadingWind.SetWindState(false);
    }
    public void SetProgress(float value)
    {
        loadingWind.SetProgress(value);
    }

}