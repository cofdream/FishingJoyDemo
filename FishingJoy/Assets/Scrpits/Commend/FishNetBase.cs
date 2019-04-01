using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//渔网基类
public class FishNetBase : MonoBehaviour
{
    public float radius = 0.2f;
    public float gunMoney = 1;

    public virtual void Init()
    {
        Collider2D[] allColl = Physics2D.OverlapCircleAll(transform.position, radius, 1 << 9);
        int length = allColl.Length;
        for (int i = 0; i < length; i++)
        {
            allColl[i].GetComponentInParent<FishBase>().Die();
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