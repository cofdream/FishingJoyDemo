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
    private Button btn_Gold_100;
    private Button btn_Gold_1000;
    private Button btn_Diamond_10;
    private Button btn_Diamond_100;
    #endregion


    protected override void InitWind()
    {
        base.InitWind();
        InitUI();

        MainSys.Instance.PauseGame();
        GameRoot.Instance.SetTimeState(false);//暂停时间

        Debug.Log("Init BuyWind Done.");
    }
    private void InitUI()
    {
        if (isInitUI == false) return;
        isInitUI = false;
        btn_Close = GetComp<Button>("bg/btn_Close");
        btn_Close.onClick.AddListener(OnClickClose);
        btn_Close = GetComp<Button>("bg/btn_Close");
        btn_Close.onClick.AddListener(OnClickClose);

        btn_Gold_100 = GetComp<Button>("BuyScrollView/Buy/ItemGold_100/UseValue");
        btn_Gold_100.onClick.AddListener(OnClickGold_1);
        btn_Gold_1000 = GetComp<Button>("BuyScrollView/Buy/ItemGold_1000/UseValue");
        btn_Gold_1000.onClick.AddListener(OnClickGold_2);
        btn_Diamond_10 = GetComp<Button>("BuyScrollView/Buy/ItemDiamond_10/UseValue");
        btn_Diamond_10.onClick.AddListener(OnClickDiamond_1);
        btn_Diamond_100 = GetComp<Button>("BuyScrollView/Buy/ItemDiamond_100/UseValue");
        btn_Diamond_100.onClick.AddListener(OnClickDiamond_2);

    }


    #region Button

    private void OnClickClose()
    {
        audioSvc.PlayUIAudio(PathDefine.UIClick);
        SetWindState(false);
        MainSys.Instance.ContinueGame();
        GameRoot.Instance.SetTimeState(true);
    }

    private void OnClickGold_1() {
        audioSvc.PlayUIAudio(PathDefine.UIClick);
        MainSys.Instance.BuyGold(100);
        OnClickClose();
    }
    private void OnClickGold_2() {
        audioSvc.PlayUIAudio(PathDefine.UIClick);
        MainSys.Instance.BuyGold(1000);
        OnClickClose();
    }
    private void OnClickDiamond_1() {
        audioSvc.PlayUIAudio(PathDefine.UIClick);
        MainSys.Instance.BuyDiamond(10);
        OnClickClose();
    }
    private void OnClickDiamond_2() {
        audioSvc.PlayUIAudio(PathDefine.UIClick);
        MainSys.Instance.BuyDiamond(100);
        OnClickClose();
    }

    #endregion
}
