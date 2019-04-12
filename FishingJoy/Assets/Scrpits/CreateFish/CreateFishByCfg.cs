/****************************************************
    文件：CreateFishByCfg_01.cs
	作者：cofdream
    邮箱: cofdream@sina.com
    日期：2019-04-11-17:03:07
	功能：生成鱼群基于配置文件
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateFishByCfg : MonoBehaviour
{
    protected string rootPath = PathDefine.FishRootPath;//创建路径
    protected bool isFirstCreate;//是否第一次创建
    public bool isCreate;//是否可以创建
    protected float curTime;//当前创建时间

    protected bool iceState;//冰冻状态
    //生成鱼的基本配置
    protected FishCfg cfg;
    protected int ID;
    protected float maxCreateTime;
    protected FishSceneSys fishSys;
    protected ObjectPool pool;

    public void InitCfg(FishCfg cfg, int id, FishSceneSys fishSys = null)
    {
        this.cfg = cfg;
        this.ID = id;
        this.fishSys = fishSys;
        if (this.fishSys == null)
        {
            this.fishSys = FishSceneSys.Instance;
        }

        maxCreateTime = cfg.MaxCreateTime;
        pool = ObjectPool.Instance;
        iceState = false;
        isCreate = false;
        isFirstCreate = true;//TODO后面修改为配置文件来设置
    }

    private void Update()
    {
        if (isCreate && iceState == false)
        {
            //计时
            curTime += Time.unscaledDeltaTime;
            if (curTime >= maxCreateTime)
            {
                curTime = 0;
                CreateFish();
            }
        }
    }

    protected void AddCreateFishCount()//添加创建的鱼的数量 --用于确定鱼的层级
    {
        fishSys.AddCreateFish();
    }
    protected int GetFishOrderInLayer()
    {
        return fishSys.GetFishOrderLayer();
    }

    public virtual void CreateFish()//创建鱼群
    {
        if (cfg == null) return;
        int length = cfg.FishPahArray.Length;
        for (int i = 0; i < length; i++)
        {
            AddCreateFishCount();

            string path = rootPath + cfg.FishPahArray[i];

            GameObject go = pool.Get(path);
            go.name = path;
            go.transform.SetParent(transform);//暂时创建在自己下面
            go.transform.position = cfg.FishPosArray[i] + cfg.BasePos;//加上基本坐标类似父类的坐标
            go.transform.localEulerAngles = cfg.FishRotateArray[i];

            FishBase fish = go.GetComponent<FishBase>();
            fish.InitFish();
            fish.InitFishMove(cfg.MoveDirArray[i], 1.2f);
            fish.InitFishRotate(0, 10, 10f, 0.5f);//鱼的转向先用默认的 后面选择是否通过配置来设置旋转信息 
            fish.SetFishOrderInLayer(GetFishOrderInLayer());
        }

    }
    public virtual void FirstCreatFish()//创建第一波鱼群
    {
        if (isFirstCreate)
        {
            CreateFish();
        }
    }
    public virtual void SetCreateState(bool state)//设置创建状态
    {
        isCreate = state;
    }
    public virtual void SetIceState(bool state)//设置冰冻状态
    {
        iceState = state;
    }
}
