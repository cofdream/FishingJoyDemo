using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//开始业务系统
public class StartSys : MonoBehaviour
{
    public static StartSys Instance { get; private set; }

    private StartWind startWind;
    private LoadingWind loadingWind;
    private DataSvc dataSvc;


    public void InistSys()
    {
        Instance = this;
        startWind = transform.Find("Canvas/StartWind").GetComponent<StartWind>();
        loadingWind = transform.Find("Canvas/LoadingWind").GetComponent<LoadingWind>();
        dataSvc = DataSvc.Instance;



        Debug.Log("Init StartSys Done.");
    }

    public void EnterStart()//进入开始场景
    {
        OpenStartWind();
        SetStartAudio();//设置场景音效
        StarsTwinkleEf();//开始星星特效
    }
    public void ExitStart()//退出开始场景
    {
        EndStarsTwinkleEf();
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
        AudioSvc.Instance.SetBgAudioVolume(dataSvc.pd.BgVolume);
        AudioSvc.Instance.SetUIAudioVolume(dataSvc.pd.UIVolume);
    }

    #region 生成一些场景特效
    //星星特效
    private void StarsTwinkleEf()
    {
        startWind.StarsTwinkleEf();
    }
    private void EndStarsTwinkleEf()
    {
        startWind.EndTwinkleEf();
    }

    #endregion
}