using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
public class GameRoot : MonoBehaviour
{
    private LoadingWind loadingWind;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        loadingWind = transform.Find("Canvas/LoadingWind").GetComponent<LoadingWind>();

        Init();
    }

    private void Update()
    {

    }

    void Init()
    {
        loadingWind.Init();
    }
}