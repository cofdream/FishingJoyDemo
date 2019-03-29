using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//开始业务系统
public class StartSys : MonoBehaviour
{
    public static StartSys Instance { get; private set; }

    private StartWind startWind;
    private LoadingWind loadingWind;

    public void InistSys()
    {
        Instance = this;
        startWind = transform.Find("Canvas/StartWind").GetComponent<StartWind>();
        loadingWind = transform.Find("Canvas/LoadingWind").GetComponent<LoadingWind>();
        

        Debug.Log("Init StartSys Done.");
    }

    public void EnterStart()//进入开始场景
    {
        OpenStartWind();
        //TODO设置场景音效
    }

    public void EnterGame()//进入游戏场景
    {
        CloseStartWind();
        MainSys.Instance.EnterGame();
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