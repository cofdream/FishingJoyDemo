using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//鱼
public class Fish : FishBase
{

    protected override void MyOnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FishWall")
        {
            Put();
        }
    }

    public override void Die(bool isFishing = false)
    {
        base.Die(isFishing);
        if (isFishing)
        {
            //生成金币或者钻石
            MainSys.Instance.CreateGoldAndDimand(transform,fishGold, fishDiamond);
        }
    }

}