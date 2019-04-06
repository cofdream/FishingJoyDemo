using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//创建鱼群的基类
public class CreateFishBase : MonoBehaviour
{
    protected string rootPath = PathDefine.FishRootPath;
    public bool isCreate;
    protected bool iceState;

    public virtual void Init()
    {
        isCreate = false;
        iceState = false;
    }

    public virtual void CreateFish()
    {
        //保存当前生成鱼的层级
        FishSceneSys.Instance.AddCreateFish();
    }
    public virtual void SetCreateState(bool state)
    {
        isCreate = state;
    }
    public virtual void SetIceState(bool state)
    {
        iceState = state;
    }
}