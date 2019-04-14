using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//渔网基类
public class FishNetBase : MonoBehaviour
{
    public float radius;
    public int gunMoney;

    public virtual void Init()//生成渔网以后的初始化
    {
        FishBase fishBase;
        float fishValue;
        Collider2D[] allColl = Physics2D.OverlapCircleAll(transform.position, radius, 1 << 9);
        int length = allColl.Length;
        for (int i = 0; i < length; i++)
        {
            fishBase = allColl[i].GetComponentInParent<FishBase>();
            fishValue = DataSvc.GetFishingProbability(gunMoney, fishBase.fishGold);
            if (Random.Range(0, 1f) < fishValue)
            {
                fishBase.Die();
            }
            else
            {
                //让当前鱼停顿一下/改变一下透明度
                fishBase.GetComponent<Move>().Pause(0.05f);
            }

        }
        if (allColl.Length == 0)
        {
            Debug.Log("null Fish");
        }
        //初始化渐变特效
        Ef_Flicker_2D ef = GetComponent<Ef_Flicker_2D>();
        ef.Init();
        ef.SetEf(0.8f);

        Invoke("Put", 2f);
    }
    private void Put()
    {
        ObjectPool.Instance.Put(name, gameObject);
    }
}