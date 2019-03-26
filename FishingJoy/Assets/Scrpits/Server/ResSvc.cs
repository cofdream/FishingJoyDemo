using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//资源加载服务
public class ResSvc : MonoBehaviour
{
    public static ResSvc Instance { get; private set; }
    private Action prgCB;
    private float AsyncProgress;

    public void Init()
    {
        Instance = this;
        prgCB = null;
        Debug.Log("Init ResSvc Done.");
    }

    public void MyUpdate()
    {
        if (prgCB != null)
        {
            prgCB();
        }
    }

    //加载图片资源
    public Sprite LoadSprite(string path)
    {
        Sprite sp = Resources.Load<Sprite>(path);
        return sp;
    }
    public Sprite[] LoadSprites(string path)
    {
        Sprite[] spArray = Resources.LoadAll<Sprite>(path);
        return spArray;
    }

    //从Resource加载物体
    public GameObject GetPrefabs(string path)
    {
        GameObject go = Resources.Load<GameObject>(path);
        return go;
    }

    //异步加载场景
    public void LoadSceneAsync(string sceneName, Action callBack = null)
    {
        //打开加载进度窗口
        StartSceneMgr start = StartSceneMgr.Instance;
        start.OpenLoadingWind();

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        prgCB += () =>//监听异步加载资源的进度 并刷新显示的百分百
        {
            AsyncProgress = async.progress;
            //设置进度
            start.SetProgress(AsyncProgress);

            if (AsyncProgress == 1f)
            {
                if (callBack != null) callBack();
                async = null;
                prgCB = null;
                start.CloseLoadingWind();
            }
        };
    }
}