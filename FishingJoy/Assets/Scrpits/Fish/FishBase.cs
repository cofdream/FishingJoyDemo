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

    protected bool isAarrested = false;//是否被捕

    public virtual void Init()
    {
        if (isInit == false)
        {
            isInit = true;
            ani = GetComponentInChildren<Animator>();
            boxCollider2D = GetComponentInChildren<BoxCollider2D>();
        }
        SetState();
    }

    protected AnimatorStateInfo animations;
    private void Update()
    {
        if (isAarrested)
        {
            animations = ani.GetCurrentAnimatorStateInfo(0);
            if (animations.normalizedTime >= 1 && animations.IsName("Die"))
            {
                isAarrested = false;
                Put();
            }
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FishWall")
        {
            Put();
        }
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "SeaWave")
        {
            Put();
        }
    }

    public void Die(bool isAarrested =true)//死亡方法，是否被抓捕致死
    {
        this.isAarrested = isAarrested;
        if (isAarrested)
        {
            SetState(false);

            //生成金币或者钻石
            MainSys.Instance.CreateGoldAndDimand(transform, fishGold, fishDiamond);
            //增加经验
            MainSys.Instance.AddExp(fishGold);
        }
        else
        {
            Put();
        }
    }
    protected virtual void SetState(bool state = true)//设置鱼的激活状态
    {
        if (state)
        {
            ani.SetBool("IsDie", false);
            boxCollider2D.enabled = true;
        }
        else
        {
            ani.SetBool("IsDie", true);
            boxCollider2D.enabled = false;
        }
    }
    public virtual void Put()//回收
    {
        ObjectPool.Instance.Put(name, gameObject);
    }

}