using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = UnityEngine.Object;

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
        InitFishCfg();//初始化创建鱼的配置数据

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

    //音效加载
    Dictionary<string, AudioClip> allAudio = new Dictionary<string, AudioClip>();
    public AudioClip LoadClip(string path, bool isCache = false)
    {
        AudioClip clip;
        if (allAudio.TryGetValue(path, out clip) == false)
        {
            clip = Resources.Load<AudioClip>(path);
            if (isCache)
            {
                allAudio.Add(path, clip);
            }
        }
        return clip;
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
    //加载场景
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    //读取配置表格数据
    private Dictionary<int, FishCfg> FishCfgDic = new Dictionary<int, FishCfg>();
    private void InitFishCfg()
    {
        TextAsset testA = Resources.Load<TextAsset>(PathDefine.FishCfg);

        XmlDocument xml = new XmlDocument();
        xml.LoadXml(testA.text);
        XmlNodeList nodesList = xml.SelectSingleNode("root").ChildNodes;

        int count = nodesList.Count;
        for (int i = 0; i < nodesList.Count; i++)
        {
            XmlElement xele = nodesList[i] as XmlElement;
            if (xele.GetAttributeNode("ID") == null)
            {
                continue;
            }
            int id = Convert.ToInt32(xele.GetAttributeNode("ID").InnerText);


            FishCfg cfg = new FishCfg();
            cfg.ID = id;

            foreach (XmlElement temp in nodesList[i].ChildNodes)
            {
                switch (temp.Name)
                {
                    case "FishPahArray":
                        cfg.FishPahArray = temp.InnerText.Split('#');
                        break;
                    case "FishPosArray":
                        cfg.FishPosArray = MySplitList(temp.InnerText);
                        break;
                    case "FishRotateArray":
                        cfg.FishRotateArray = MySplitList(temp.InnerText);
                        break;
                    case "IsFishCreate":
                        cfg.IsFishCreate = bool.Parse(temp.InnerText);
                        break;
                    case "MaxCreateTime":
                        cfg.MaxCreateTime = int.Parse(temp.InnerText);
                        break;
                    case "MoveDir":
                        cfg.MoveDir = MySplit(temp.InnerText);
                        break;
                }
            }
            FishCfgDic.Add(id, cfg);
        }
    }
    public FishCfg GetFishCfg(int id)
    {
        FishCfg cfg = null;
        FishCfgDic.TryGetValue(id, out cfg);
        return cfg;
    }
    public Dictionary<int, FishCfg> GetFishCfgDic()
    {
        return FishCfgDic;
    }
    private Vector3 MySplit(string str)//切割
    {
        string[] temp = str.Split(',');

        Vector3 v3 = new Vector3();

        if (temp.Length >= 3)
        {
            float x = float.Parse(temp[0]);
            float y = float.Parse(temp[1]);
            float z = float.Parse(temp[2]);
            v3 = new Vector3(x, y, z);
        }
        return v3;
    }
    private Vector3[] MySplitList(string str)//切割数组
    {
        string[] strList = str.Split('#');
        Vector3[] v3List = new Vector3[strList.Length];
        for (int i = 0; i < strList.Length; i++)
        {
            v3List[i] = MySplit(strList[i]);
        }
        return v3List;
    }
}