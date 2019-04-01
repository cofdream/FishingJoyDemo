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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MyOnTriggerEnter2D(collision);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        MyOnCollisionEnter2D(collision);
    }

    protected virtual void MyOnTriggerEnter2D(Collider2D collision)
    {

    }
    protected virtual void MyOnCollisionEnter2D(Collision2D collision)
    {

    }
}