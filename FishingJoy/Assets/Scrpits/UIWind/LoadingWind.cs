using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//加载界面
public class LoadingWind : MonoBehaviour
{

    public static LoadingWind Instance { get; private set; }
    private Image slider;

    private void Awake()
    {
        Instance = this;

        slider = transform.Find("buttom/progress/fg").GetComponent<Image>();
    }

    private void SetWindState(bool state = true)
    {
        gameObject.SetActive(state);
    }

    public void OpenLoadingWind()
    {
        SetWindState();
    }
    public void CloseLoadingWind()
    {
        SetWindState(false);
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