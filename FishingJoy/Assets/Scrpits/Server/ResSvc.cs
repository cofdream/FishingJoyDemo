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

    private void Awake()
    {
        Instance = this;
        prgCB = null;
    }

    private void Update()
    {
        if (prgCB != null)
        {
            prgCB();
        }
    }


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

    public void LoadSceneAsync(string sceneName, Action callBack = null)
    {
        //打开加载进度窗口
        //TODO

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        prgCB += () =>//监听异步加载资源的进度 并刷新显示的百分百
        {
            AsyncProgress = async.progress;
            //设置进度
            //TODO

            if (AsyncProgress == 1f)
            {
                if (callBack != null) callBack();
                async = null;
                prgCB = null;

                //关闭加载进度窗口
                //TODO
            }
        };
    }
}