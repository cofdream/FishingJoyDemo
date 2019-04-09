using DG.Tweening;
using System;
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

            InitStarsTwinkleEf();//初始化星星特效动画
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

    //星星特效
    private void InitStarsTwinkleEf()
    {
        ForeachStarArray((i) =>
        {
            starArray[i].rectTransform.DOLocalRotate(new Vector3(0, 0, 180f), 1.8f, RotateMode.WorldAxisAdd).SetLoops(-1, LoopType.Yoyo);
            starArray[i].DOColor(new Color(1f, 1f, 1f, 0.2f), 1f).SetLoops(-1, LoopType.Yoyo);
            starArray[i].rectTransform.DOPause();
        });
    }
    private void ForeachStarArray(Action<int> callBack)
    {
        int length = starArray.Length;
        for (int i = 0; i < length; i++)
        {
            callBack(i);
        }
    }
    public void StarsTwinkleEf()
    {
        ForeachStarArray((i) =>
        {
            starArray[i].rectTransform.DOPlay();
        });
    }
    public void EndTwinkleEf()
    {
        ForeachStarArray((i) =>
        {
            starArray[i].rectTransform.DOPause();
        });
    }
}