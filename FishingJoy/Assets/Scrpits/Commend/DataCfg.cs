/****************************************************
    文件：DataCfg.cs
	作者：cofdream
    邮箱: cofdream@sina.com
    日期：2019-04-11-15:49:04
	功能：数据配置基类
*****************************************************/

using UnityEngine;

public class DataCfg<T>
{
    public int ID;
}

public class FishCfg : DataCfg<FishCfg>
{
    public string[] FishPahArray;
    public Vector3[] FishPosArray;
    public Vector3[] FishRotateArray;
    public Vector3[] MoveDirArray;
    public bool IsFishCreate;
    public float MaxCreateTime;
    public Vector3 BasePos;
}
