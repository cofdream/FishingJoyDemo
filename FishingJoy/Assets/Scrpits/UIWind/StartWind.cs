using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StartWind : WindBase
{
    #region UI
    private bool isUI = true;
    private Transform allStar;
    private Button btn_Start;
    #endregion

    private void Start()
    {
        
    }
    private StartSys startSys;

    protected override void InitWind()
    {
        base.InitWind();
        startSys = StartSys.Instance;
        InitUI();
        RefreshUI();

        Debug.Log("Init StartWind Done.");
    }
    protected override void Clear()
    {
        base.Clear();
        startSys = null;
    }
    private void InitUI()
    {
        if (isUI)
        {
            isUI = false;
            allStar = GetComp<Transform>("top/star");
            btn_Start = GetComp<Button>("center/btn_Start");
            btn_Start.onClick.AddListener(OnClickStart);
        }
    }


    public void RefreshUI()
    {
        //TODO设置开始按钮 显示文字为 继续游戏/开始游戏

        //TODO 开始界面可以设置音效
        //设置音量的默认值
    }

    private void OnClickStart()
    {
        audioSvc.PlayUIAudio(PathDefine.UIClick);
        audioSvc.StopBgAudio();//停止背景音效
        startSys.ExitStart();//退出游戏场景
        //加载场景
        resSvc.LoadSceneAsync(PathDefine.MainScene, () =>
        {
            startSys.EnterGame();//进入游戏场景 加载游戏相关配置
        });
    }
}