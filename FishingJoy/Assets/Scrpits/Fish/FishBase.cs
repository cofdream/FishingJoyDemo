using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//鱼的基类
public class FishBase : MonoBehaviour
{
    protected Animator ani;
    protected BoxCollider2D boxCollider2D;
    protected bool isInit = false;

    public int fishGold;
    public int fishDiamond;

    public virtual void Init()
    {
        if (isInit == false)
        {
            isInit = true;
            ani = GetComponentInChildren<Animator>();
            boxCollider2D = GetComponentInChildren<BoxCollider2D>();
        }
        Create();
    }

    protected AnimatorStateInfo animations;
    private void Update()
    {
        animations = ani.GetCurrentAnimatorStateInfo(0);
        if (animations.normalizedTime >= 1 && animations.IsName("Die"))
        {
            Put();
        }
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

    protected virtual void Create()
    {
        ani.SetBool("IsDie", false);
        boxCollider2D.enabled = true;
    }
    public virtual void Die(bool isFishing)//死亡方法
    {
        ani.SetBool("IsDie", true);
        boxCollider2D.enabled = false;
    }
    protected virtual void Put()
    {
        ObjectPool.Instance.Put(name, gameObject);
    }

}