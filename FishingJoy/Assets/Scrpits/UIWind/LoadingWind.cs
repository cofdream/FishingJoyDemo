using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//加载界面
public class LoadingWind : WindBase
{
    private Image slider;

    private bool isInitWind = true;

    public override void Init()
    {
        InitWind();

        Debug.Log("Init LoadingWind Done.");
    }

    private void InitWind()
    {
        if (isInitWind == false) return;
        isInitWind = false;

        slider = transform.Find("buttom/progress/fg").GetComponent<Image>();
    }


    public void SetProgress(float value)
    {
        if (slider == null)
        {
            return;
        }
        slider.fillAmount = value;
    }
}