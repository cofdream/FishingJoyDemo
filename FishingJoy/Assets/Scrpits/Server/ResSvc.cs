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
    private StartSys startSys;
    private float AsyncProgress;

    public void InitSvc()
    {
        Instance = this;
        prgCB = null;
        startSys = StartSys.Instance;
        allSp = new Dictionary<string, Sprite>();
        allSps = new Dictionary<string, Sprite[]>();

        Debug.Log("Init ResSvc Done.");
    }

    public void Update()
    {
        if (prgCB != null)
        {
            prgCB();
        }
    }

    //加载图片资源
    Dictionary<string, Sprite> allSp = new Dictionary<string, Sprite>();
    public Sprite LoadSprite(string path, bool isCache = false)
    {
        Sprite tempSp;
        if (allSp.TryGetValue(path, out tempSp) == false)
        {
            tempSp = Resources.Load<Sprite>(path);
            if (isCache)
            {
                allSp.Add(path, tempSp);
            }
        }
        return tempSp;
    }
    Dictionary<string, Sprite[]> allSps = new Dictionary<string, Sprite[]>();
    public Sprite[] LoadSprites(string path, bool isCache = false)
    {
        Sprite[] spArray;
        if (allSps.TryGetValue(path, out spArray) == false)
        {
            spArray = Resources.LoadAll<Sprite>(path);
            if (isCache)
            {
                allSps.Add(path, spArray);
            }
        }
        return spArray;
    }

    //从Resource加载物体并且实例化
    public GameObject LoadPrefab(string path)
    {
        GameObject go = Resources.Load<GameObject>(path);
        go = Instantiate(go);
        return go;
    }

    //异步加载场景
    public void LoadSceneAsync(string sceneName, Action callBack = null)
    {
        //打开加载进度窗口
        startSys.OpenLoadingWind();

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        prgCB += () =>//监听异步加载资源的进度 并刷新显示的百分百
        {
            AsyncProgress = async.progress;
            startSys.SetProgress(AsyncProgress);//设置进度

            if (AsyncProgress == 1f)
            {
                if (callBack != null) callBack();
                async = null;
                prgCB = null;
                startSys.CloseLoadingWind();
            }
        };
    }
}