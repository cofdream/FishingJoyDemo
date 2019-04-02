using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//普通子弹
public class Bullet : BulletBase
{

    protected override void MyOnTriggerEnter2D(Collider2D collision)
    {
        base.MyOnTriggerEnter2D(collision);
        if (collision.tag == "Wall")
        {
            Die(false);
            print("子弹触发到了" + collision.transform.parent.name);
        }
    }

    protected override void MyOnCollisionEnter2D(Collision2D collision)
    {
        base.MyOnCollisionEnter2D(collision);
        if (collision.transform.tag == "Fish")
        {
            Die();
        }
        else
        {
            print("子弹撞到了" + collision.transform.parent.name);
        }
    }

    protected override void Die(bool isCreateNet = true)
    {
        base.Die(isCreateNet);
        if (isCreateNet)
        {
            //生成网
            MainSys.Instance.CreateNetFish(transform.position, fishNetName);
        }
        ObjectPool.Instance.Put(name, gameObject);
    }
}