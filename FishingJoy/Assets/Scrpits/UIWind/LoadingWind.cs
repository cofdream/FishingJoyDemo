using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//加载界面
public class LoadingWind : WindBase
{
    private Image slider;

    protected override void InitWind()
    {
        base.InitWind();

        slider = GetComp<Image>("buttom/progress/fg");

        Debug.Log("Init LoadingWind Done.");
    }


    public void SetProgress(float value)
    {
        slider.fillAmount = value;
    }
}