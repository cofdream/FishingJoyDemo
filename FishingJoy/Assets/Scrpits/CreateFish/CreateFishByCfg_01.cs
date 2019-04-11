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

public class CreateFishByCfg_01 : CreateFishBase
{

    public string[] fishPath;
    public Vector3[] fishPos;

    public Vector3[] fishRotateArray;

    public float maxTime;

    private int ID = 1;

    public override void Init()
    {
        base.Init();
        
        ResSvc resSvc = ResSvc.Instance;

        FishCfg cfg = resSvc.GetFishCfg(ID);

        fishPath = cfg.FishPahArray;
        fishPos = cfg.FishPosArray;
        fishRotateArray = cfg.FishRotateArray;
    }

    public  void
}
