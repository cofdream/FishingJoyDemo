using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//提示界面
public class TipsWind : WindBase
{
    private Text value;
    private bool isUI = true;

    protected override void InitWind()
    {
        base.InitWind();
        InitUI();

        Debug.Log("Init Tips Done.");
    }
    private void InitUI()
    {
        if (isUI)
        {
            isUI = false;
            value = transform.Find("value").GetComponent<Text>();
        }
    }

    private void Update()
    {

    }

    public void Tips(string value)
    {
        gameObject.SetActive(true);
        this.value.text = value;
        Invoke("CloseTips", 2f);
    }
    public void CloseTips()
    {
        SetWindState(false);
    }
}