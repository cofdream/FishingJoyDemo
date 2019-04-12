using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//渔场业务
public class FishSceneSys : MonoBehaviour
{
    public static FishSceneSys Instance { get; private set; }

    private List<CreateFishByCfg> allCreateFishList;
    private int allCreateCount; //鱼群的脚本的数量
    private int CurFishCount;

    public void Init()
    {
        Instance = this;
    }

    public void EnterFishScene()//进入渔场
    {
        InintCreateFishCfg();//初始化创建鱼群配置的数据 TODO后面修改为 Init初始化
        SetAllCreateFishListState();
        CreateFish();//初次进入场景 生产一群鱼群
    }
    public void QuitFishScene()
    {
        SetAllCreateFishListState(false);
        ClearAllFish();//清除场景中的鱼群
    }

    public void PauseFishScene()
    {
        SetAllCreateFishListState(false);
        SetFishListBehaviourState(false);
    }
    public void ContinueGame()//配合暂停使用
    {
        SetAllCreateFishListState();
        SetFishListBehaviourState();
    }

    public void InintCreateFishCfg()//初始化创建鱼群配置的数据
    {
        var cfgDic = ResSvc.Instance.GetFishCfgDic();
        allCreateFishList = new List<CreateFishByCfg>();

        foreach (var item in cfgDic)
        {
            //一个鱼群放到一个物体下面 PS:不然鱼群移动会不协调 具体不清楚上面原因的导致，目前这样做
            GameObject createParent = new GameObject();
            createParent.transform.SetParent(transform);

            CreateFishByCfg cfg = createParent.AddComponent<CreateFishByCfg>();
            cfg.InitCfg(item.Value, item.Key);
            allCreateFishList.Add(cfg);
        }
    }
    public void SetAllCreateFishListState(bool state = true)//所有设置鱼群的创建状态
    {
        int length = allCreateFishList.Count;
        for (int i = 0; i < length; i++)
        {
            allCreateFishList[i].SetCreateState(state);
        }
    }
    public void CreateFish() //创建第一波鱼群
    {
        int length = allCreateFishList.Count;
        for (int i = 0; i < length; i++)
        {
            allCreateFishList[i].FirstCreatFish();
        }
    }
    public void ClearAllFish() //清除所有的鱼
    {
        int length = allCreateFishList.Count;
        for (int i = length - 1; i >= 0; i--)
        {
            allCreateFishList[i].ClearAllFish();
        }
    }
    public void SetFishListBehaviourState(bool state = true) //设置鱼群的行为状态
    {
        int length = allCreateFishList.Count;
        for (int i = length - 1; i >= 0; i--)
        {
            allCreateFishList[i].SetFishListBehaviourState();
        }
    }

    public virtual void IceStateStopMove(float time) //冰冻状态停止移动
    {
        int length = allCreateFishList.Count;
        for (int i = length - 1; i >= 0; i--)
        {
            allCreateFishList[i].IceStateStopMove(time);
        }
    }

    public void AddCreateFish(int count = 1) //设置鱼群的层级
    {
        CurFishCount += count;
        //到达最大回归0
        if (CurFishCount >= short.MaxValue)
        {
            CurFishCount = 0;
        }
    }
    public int GetFishOrderLayer()
    {
        return CurFishCount;
    }

}