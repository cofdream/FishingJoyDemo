using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//创建鱼群的基类
public class CreateFishBase : MonoBehaviour
{
    protected string rootPath = PathDefine.FishRootPath;
    public bool isCreate;
    protected bool iceState;
    protected float curTime;

    public Vector3 moveDirection;
    public float fishSpeed;

    public virtual void Init()
    {
        isCreate = false;
        iceState = false;
    }

    public virtual void CreateFish()
    {
        FishSceneSys.Instance.AddCreateFish();//保存当前生成鱼的层级
    }
    public virtual void SetCreateState(bool state)
    {
        isCreate = state;
    }
    public virtual void SetIceState(bool state)
    {
        iceState = state;
    }
    public virtual void ClearAllFish()//清除所有的鱼
    {
        int count = transform.childCount;
        for (int i = count - 1; i >= 0; i--)
        {
            Fish fish = transform.GetChild(i).GetComponent<Fish>();
            if (fish != null)
            {
                fish.Put();
            }
        }
    }
}