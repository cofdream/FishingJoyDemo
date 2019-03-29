using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//子弹基类
public class BulletBase : MonoBehaviour
{
    public int lv;


    protected virtual void Die(bool isCreateNet = true)//子弹销毁方法
    {
        
    }
}