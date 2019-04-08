using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StartWind : WindBase
{
    #region UI
    private bool isUI = true;
    private Image[] starArray;
    private Button btn_Start;
    #endregion

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
            btn_Start = GetComp<Button>("center/btn_Start");
            btn_Start.onClick.AddListener(OnClickStart);

            Transform starParent = GetComp<Transform>("top/star");
            starArray = new Image[starParent.childCount];
            for (int i = 0; i < starParent.childCount; i++)
            {
                starArray[i] = starParent.GetChild(i).GetComponent<Image>();
            }
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
        startSys.ExitStart();//退出游戏场景
        startSys.EnterGame();//进入游戏场景 加载游戏相关配置
    }


    public void StarsTwinkleEf()
    {
        int length = starArray.Length;
        for (int i = 0; i < length; i++)
        {
            starArray[i].rectTransform.DOLocalRotate(new Vector3(0, 0, 180f), 1.8f, RotateMode.WorldAxisAdd).SetLoops(-1, LoopType.Yoyo);
            starArray[i].DOColor(new Color(1f, 1f, 1f, 0.2f), 1f).SetLoops(-1, LoopType.Yoyo);

        }
    }
    public void EndTwinkleEf()
    {

    }
}