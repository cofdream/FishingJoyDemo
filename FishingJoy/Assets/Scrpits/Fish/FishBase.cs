using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//鱼的基类
public class FishBase : MonoBehaviour
{
    protected Animator ani;
    protected BoxCollider2D boxCollider2D;
    protected AnimatorStateInfo animations;
    protected SpriteRenderer spRenderer;
    protected FishRotate fishRotate;
    protected Move fisMove;
    protected bool isInit = false;

    public int fishGold;
    public int fishDiamond;

    protected bool isAarrested = false;//是否被捕

    //初始化
    public virtual void InitFish()//初始化鱼的配置
    {
        if (isInit == false)
        {
            isInit = true;
            ani = GetComponentInChildren<Animator>();
            boxCollider2D = GetComponentInChildren<BoxCollider2D>();
            spRenderer = GetComponentInChildren<SpriteRenderer>();
            fishRotate = gameObject.AddComponent<FishRotate>();
            fisMove = gameObject.AddComponent<Move>();
        }
        SetState();
    }
    public virtual void InitFishMove(Vector3 direction, float speed)
    {
        fisMove.Init(direction, speed);
    }
    public virtual void InitFishRotate(int minZ, int maxZ, float maxTime, float speed, bool state = true)//初始鱼的旋转信息
    {
        fishRotate.Init(minZ, maxZ, maxTime, speed, state);
    }
    public void SetFishOrderInLayer(int layer)//设置鱼的层级
    {
        spRenderer.sortingOrder = layer;
    }

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

    //物理碰撞/触发
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

    //死亡方法
    public void Die(bool isAarrested = true)//死亡方法，是否被抓捕致死
    {
        this.isAarrested = isAarrested;
        SetState(false);
        if (isAarrested)
        {
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
    protected virtual void Put()//回收
    {
        ObjectPool.Instance.Put(name, gameObject);
    }

    //设置鱼的行为状态 --鱼是否可以移动&旋转
    public void SetFishBehaviour(bool state = true)
    {
        fisMove.SetMoveState(state);
        fishRotate.SetState(state);
    }
    public void StopFishBehaviour(float time)//暂时停止鱼的行为状态 -停止一段时间鱼不可以移动&旋转
    {
        fisMove.Pause(time);
        fishRotate.Pause(time);
    }
   
}