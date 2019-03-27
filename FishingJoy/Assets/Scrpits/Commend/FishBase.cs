using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//鱼的基类
public class FishBase : MonoBehaviour
{
    protected Animator ani;
    protected BoxCollider2D boxCollider2D;
    protected bool isInit = false;

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

    private void Update()
    {
        AnimatorStateInfo animations = ani.GetCurrentAnimatorStateInfo(0);
        if (animations.normalizedTime >= 1 && animations.IsName("Die"))
        {
            ObjectPool.Instance.Put(name, gameObject);
        }
    }

    public virtual void Create()
    {
        ani.SetBool("IsDie", false);
        boxCollider2D.enabled = true;
    }
    public virtual void Die()
    {
        ani.SetBool("IsDie", true);
        boxCollider2D.enabled = false;
    }
}