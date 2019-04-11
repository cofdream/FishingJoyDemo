/****************************************************
  文件：BuyWind.cs
  作者：cofdream
  邮箱: cofdream@sina.com
  日期：2019-04-11-10:28:51
  功能：购买界面
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyWind : WindBase
{
    #region UI
    private bool isInitUI = true;

    private Button btn_Close;
    #endregion


    protected override void InitWind()
    {
        base.InitWind();
        InitUI();

        GameRoot.Instance.SetTimeState();//暂停时间

        Debug.Log("Init BuyWind Done.");
    }

    protected override void Clear()
    {
        GameRoot.Instance.SetTimeState(false);//继续时间
    }

    private void InitUI()
    {
        if (isInitUI == false) return;
        isInitUI = false;
        btn_Close = GetComp<Button>("bg/btn_Close");
        btn_Close.onClick.AddListener(OnClickClose);
    }


    #region Button

    private void OnClickClose()
    {
        audioSvc.PlayUIAudio(PathDefine.UIClick);
        SetWindState(false);
    }

    #endregion
}
