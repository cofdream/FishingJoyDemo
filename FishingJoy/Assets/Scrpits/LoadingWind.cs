using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//
public class LoadingWind : MonoBehaviour
{

    public static LoadingWind Instance { get; private set; }
    private Slider slider;

    public void Init()
    {
        Instance = this;
        slider = transform.Find("buttom/progress").GetComponent<Slider>();
    }
    private void Start()
    {

    }
    public void SetWindState(bool state = true)
    {
        gameObject.SetActive(state);
    }

    public void SetProgress(float value)
    {
        slider.value = value;
    }
}