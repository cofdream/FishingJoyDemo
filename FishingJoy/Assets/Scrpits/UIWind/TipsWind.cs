using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//提示界面
public class TipsWind : MonoBehaviour
{
    public static TipsWind Instance { get; private set; }
    private Text value;

    public void Init()
    {
        Instance = this;
        value = transform.Find("value").GetComponent<Text>();
        Debug.Log("Init Tips Done.");
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
        gameObject.SetActive(false);
    }
}