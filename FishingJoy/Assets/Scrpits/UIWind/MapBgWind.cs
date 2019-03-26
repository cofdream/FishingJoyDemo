using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//地图背景UI界面
public class MapBgWind : WindBase {

    private Image bg;
    private bool isInitWind = true;

    public override void Init()
    {
        InitWind();

        Debug.Log("Init MapBgWind Done.");
    }
    private void InitWind()
    {
        if (isInitWind == false) return;
        isInitWind = false;

        bg = transform.Find("bg").GetComponent<Image>();
    }
    public void RefreshUI()
    {
        
    }

}