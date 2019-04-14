using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//子弹基类
public class BulletBase : MonoBehaviour
{
    protected string fishNetName = PathDefine.NetPath;//渔网的基本路径名字
    protected bool isInit = true;
    protected MoveBullet bullet;


    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            Die(false);
            //print("子弹触发到了" + collision.transform.parent.name);
        }
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Fish")
        {
            Die();
        }
        else
        {
            //print("子弹撞到了" + collision.transform.parent.name);
        }
    }

    protected virtual void Die(bool isCreateNet = true)//子弹销毁方法
    {
        if (isCreateNet)
        {
            MainSys.Instance.CreateNetFish(transform.position, fishNetName); //生成网
        }
        ObjectPool.Instance.Put(name, gameObject);
    }

    public void Init()
    {
        if (isInit)
        {
            isInit = false;
            bullet = gameObject.AddComponent<MoveBullet>();
        }
    }
    public void InitBullet(Vector3 direction,float speed)
    {
        bullet.Init(direction,speed);
    }
}