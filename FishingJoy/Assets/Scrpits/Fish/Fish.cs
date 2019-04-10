using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//鱼
public class Fish : FishBase
{

    protected override void MyOnTriggerEnter2D(Collider2D collision)
    {
        base.MyOnTriggerEnter2D(collision);
        if (collision.tag == "FishWall")
        {
            Put();
        }

    }
    protected override void MyOnCollisionEnter2D(Collision2D collision)
    {
        base.MyOnCollisionEnter2D(collision);
        if (collision.transform.tag == "SeaWave")
        {
            Put();
        }
    }

    public override void BeAarrested_Die()
    {
        base.BeAarrested_Die();
        isAarrested = true;
        //生成金币或者钻石
        MainSys.Instance.CreateGoldAndDimand(transform, fishGold, fishDiamond);
        //增加经验
        MainSys.Instance.AddExp(fishGold);
    }
}