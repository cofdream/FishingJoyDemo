using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//子弹基类
public class BulletBase : MonoBehaviour
{
    public int lv;

    public virtual void Destroy()//子弹销毁方法
    {
        Destroy(gameObject);
    }
}