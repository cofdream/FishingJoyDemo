using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//子弹
public class Bullet : BulletBase
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            Die(false);
            print("子弹触发到了" + collision.transform.parent.name);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
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
        }
        ObjectPool.Instance.Put(name, gameObject);
    }
}