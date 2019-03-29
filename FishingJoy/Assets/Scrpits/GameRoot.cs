﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//游戏开始
public class GameRoot : MonoBehaviour
{
    public static GameRoot Instance { get; private set; }

    public Canvas Canvas { get; private set; }

    private void Awake()
    {
        Debug.Log("Init GameRoot Done.");
        Instance = this;
        InitWind();
        Init();

        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {

    }
    private void Init()
    {
        ResSvc resSvc = GetComponent<ResSvc>();
        ObjectPool objectPool = GetComponent<ObjectPool>();
        DataSvc dataSvc = GetComponent<DataSvc>();
        StartSys startSys = GetComponent<StartSys>();
        MainSys mainSys = GetComponent<MainSys>();

        dataSvc.InitSvc();//数据管理

        startSys.InistSys();//开始
        mainSys.InitSys();//主游戏

        resSvc.InitSvc(); //资源加载
        objectPool.Init(); //对象池

        startSys.EnterStart();//开始游戏
    }

    private void InitWind()
    {
        Canvas = transform.Find("Canvas").GetComponent<Canvas>();
        for (int i = 0; i < Canvas.transform.childCount; i++)
        {
            Canvas.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}