using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//渔网基类
public class FishNetBase : MonoBehaviour
{
    private FishBase[] fish;//渔网内的鱼

    public void Init()
    {
        Collider2D[] allColl = Physics2D.OverlapCircleAll(transform.position, transform.GetChild(0).localScale.x, 1 << 9);
        int length = allColl.Length;
        fish = new FishBase[length];
        for (int i = 0; i < length; i++)
        {
            fish[i] = allColl[i].GetComponentInParent<FishBase>();
        }
        HarmFish();

        Invoke("Die", 2f);
    }
    public void HarmFish()
    {
        for (int i = 0; i < fish.Length; i++)
        {
            fish[i].Die();
        }
    }
    private void Die()
    {
        ObjectPool.Instance.Put(name, gameObject);
    }
}