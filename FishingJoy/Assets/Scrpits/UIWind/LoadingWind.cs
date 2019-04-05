using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//加载界面
public class LoadingWind : WindBase
{
    #region UI
    private Image slider;
    private bool isUI = true;
    #endregion

    protected override void InitWind()
    {
        base.InitWind();
        InitUI();

        Debug.Log("Init LoadingWind Done.");
    }
    private void InitUI()
    {
        if (isUI)
        {
            isUI = false;
            slider = GetComp<Image>("buttom/progress/fg");
        }
    }

    public void SetProgress(float value)
    {
        slider.fillAmount = value;
    }
}