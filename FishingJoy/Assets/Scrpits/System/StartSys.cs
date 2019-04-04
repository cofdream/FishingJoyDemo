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
        //设置场景音效 
        SetStartAudio();
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

    //音效
    public void SetStartAudio()
    {
        AudioSvc.Instance.PlayBgAudio(PathDefine.BGMusic_Main);
        AudioSvc.Instance.SetBgAudioVolume(DataSvc.Instance.pd.BgVolume);
        AudioSvc.Instance.SetUIAudioVolume(DataSvc.Instance.pd.UIVolume);
    }
}