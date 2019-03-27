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
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Fish")
        {
            collision.transform.GetComponentInParent<Fish>().Die();
           
            Die();
        }
    }

    private void Die(bool isCreate = true)
    {
        if (isCreate)
        {
            //生成网
            print("生成网");
        }
        ObjectPool.Instance.Put(name, gameObject);
    }
}