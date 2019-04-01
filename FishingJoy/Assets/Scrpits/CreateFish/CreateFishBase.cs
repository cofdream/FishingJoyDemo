using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//创建鱼群的基类
public class CreateFishBase : MonoBehaviour
{
    protected string rootPath = PathDefine.FishRootPath;
    public bool IsCreate { get; set; }

    public virtual void Init()
    {

    }

    public virtual void CreateFish()
    {

    }
}