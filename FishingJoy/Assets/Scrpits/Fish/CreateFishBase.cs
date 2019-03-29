using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//创建鱼群的基类
public class CreateFishBase : MonoBehaviour
{
    //是否持续产生鱼群
    public bool isUpdate;
    //初始化创建鱼群的的配置
    public virtual void Init()
    {

    }
    //持续生成鱼的更新
    public virtual void CreateUpdate()
    {
        
    }
}