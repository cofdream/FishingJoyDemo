using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//鱼的基类
public class FishBase : MonoBehaviour
{
    protected Animator ani;
    protected BoxCollider2D boxCollider2D;
    protected bool isInit = false;

    public int fishMoney;

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

    public virtual void Create()
    {
        //ani.SetBool("IsDie", false);
        boxCollider2D.enabled = true;
    }
    public virtual void Die()
    {
        ani.SetBool("IsDie", true);
        boxCollider2D.enabled = false;
    }
    protected virtual void Put()
    {
        ObjectPool.Instance.Put(name, gameObject);
    }

}