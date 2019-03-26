using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//游戏开始
public class Game : MonoBehaviour
{
    ResSvc resSvc;
    StartSceneMgr startSceneMgr;
    GameSceneMgr gameSceneMgr;
    GameController gameController;
    private void Awake()
    {
        Debug.Log("Init Game Done.");
        Init();
        DontDestroyOnLoad(gameObject);

    }

    private void Update()
    {
        if (resSvc != null)
        {
            resSvc.MyUpdate();
        }
        if (gameSceneMgr != null)
        {
            gameSceneMgr.MyUpdata();
        }
        if (gameController != null)
        {
            gameController.MyUpdate();
        }
    }

    private void Init()
    {
        startSceneMgr = GetComponent<StartSceneMgr>();
        gameSceneMgr = GetComponent<GameSceneMgr>();
        gameController = GetComponent<GameController>();
        resSvc = GetComponent<ResSvc>();

        //资源加载服务
        resSvc.Init();

        //场景管理
        startSceneMgr.Init();
        gameSceneMgr.Init();

        //控制器
        gameController.Init();
    }
}