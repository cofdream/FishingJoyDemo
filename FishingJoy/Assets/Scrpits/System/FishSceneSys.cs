using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//渔场业务
public class FishSceneSys : MonoBehaviour
{
    public static FishSceneSys Instance { get; private set; }

    private CreateFishBase[] allCreateFishing;//所有创建鱼群的脚本呢
    private int allCreateCount;//鱼群的脚本的数量

    private int CurFishCount;
    public void Init()
    {
        Instance = this;
        InitCreatData();
    }
    private void InitCreatData() //初始化创建鱼群配置的数据
    {
        allCreateFishing = GetComponentsInChildren<CreateFishBase>();
        allCreateCount = allCreateFishing.Length;
        for (int i = 0; i < allCreateCount; i++)
        {
            allCreateFishing[i].Init();//调用鱼群生成的初始化方法
        }
    }

    public void EnterFishScene()
    {
        SetAllCreateFishingState(true);
        //初次进入场景 生产一群鱼群
        CreateFish();
    }
    public void QuitFishScene()
    {
        SetAllCreateFishingState(false);
        //清除场景中的鱼群
        ClearAllFish();
    }

    public void SetAllCreateFishingState(bool state)//设置鱼群的创建状态
    {
        for (int i = 0; i < allCreateCount; i++)
        {
            allCreateFishing[i].SetCreateState(state);
        }
    }
    public void CreateFish()//直接创建一波鱼群
    {
        for (int i = 0; i < allCreateCount; i++)
        {
            if (allCreateFishing[i].isFirstCreate)
            {
                allCreateFishing[i].CreateFish();
            }
        }
    }
    public void ClearAllFish()//清除所有的鱼
    {
        for (int i = 0; i < allCreateCount; i++)
        {
            allCreateFishing[i].ClearAllFish();
        }
    }

    public void AddCreateFish(int count = 1)//设置鱼群的层级
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

    public void SetIceSkillState(bool state)//设置鱼群的冰冻技能状态
    {
        for (int i = 0; i < allCreateCount; i++)
        {
            allCreateFishing[i].SetIceState(state);
        }
    }
    public virtual void IceStateStopMove(float time)//冰冻状态停止移动
    {
        Transform temp;
        for (int i = 0; i < allCreateCount; i++)
        {
            temp = allCreateFishing[i].transform;
            for (int j = 0; j < temp.childCount; j++)
            {
                temp.GetChild(j).GetComponent<Move>().Pause(time);
            }
        }
    }
}