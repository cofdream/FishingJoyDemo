using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//渔网基类
public class FishNetBase : MonoBehaviour
{
    public float radius;
    public int gunMoney;

    public virtual void Init()
    {
        FishBase fishBase;
        float fishValue;
        Collider2D[] allColl = Physics2D.OverlapCircleAll(transform.position, radius, 1 << 9);
        int length = allColl.Length;
        for (int i = 0; i < length; i++)
        {
            fishBase = allColl[i].GetComponentInParent<FishBase>();
            fishValue = Tools.GetFishingProbability(gunMoney, fishBase.fishGold);
            if (Random.Range(0, 1f) < fishValue)
            {
                fishBase.BeAarrested_Die();
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
        Invoke("Die", 0.5f);
    }

    private void Die()
    {
        ObjectPool.Instance.Put(name, gameObject);
    }
}