using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//游戏开始
public class Game : MonoBehaviour
{
    public static Game Instance { get; private set; }
    ResSvc resSvc;

    RectTransform canvasRect;
    StartSceneMgr startSceneMgr;
    GameSceneMgr gameSceneMgr;

    PlayerController playerCtr;
    private void Awake()
    {
        print("Awake Time.time:" + Time.time);
        Debug.Log("Init Game Done.");
        Instance = this;
        Init();
        canvasRect = transform.Find("Canvas").GetComponent<RectTransform>();

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
        if (playerCtr != null)
        {
            playerCtr.MyUpdate();
        }
    }
    private void Init()
    {
        startSceneMgr = GetComponent<StartSceneMgr>();
        gameSceneMgr = GetComponent<GameSceneMgr>();
        playerCtr = GetComponent<PlayerController>();
        resSvc = GetComponent<ResSvc>();
        

        //资源加载服务
        resSvc.Init();
        GetComponent<ObjectPool>().Init(); //对象池

        //场景管理
        startSceneMgr.Init();
        gameSceneMgr.Init();

        //玩家控制器
        playerCtr.Init();
    }

    //屏幕坐标转换成世界坐标
    public void GetWorldPointInRectangle(Vector2 targetPos, out Vector3 point)
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(canvasRect, targetPos, Camera.main, out point);
    }
}