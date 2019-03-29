using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//开始界面的UI管理
public class StartSceneMgr : MonoBehaviour
{
    public static StartSceneMgr Instance { get; private set; }

    private StartWind startWind;
    private LoadingWind loadingWind;

    private Transform Canvas;

    public void Init()
    {
        Instance = this;

        InitDone();

        RefreshUI();

        Debug.Log("Init StartSceneMgr Done.");
    }
    void InitDone()
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

    public void RefreshUI()
    {
        RefreshUI_MapBg();
        RefreshUI_StartWind();
    }

    //MapWind
    public void OpenMapBgWind()
    {
      
    }  
    public void CloseMapBgWind()
    {

    }
    public void RefreshUI_MapBg()
    {

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
    public void RefreshUI_StartWind()
    {
        startWind.RefreshUI();
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