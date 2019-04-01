using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//创建指定类型的鱼 
//每过一段时间 创建出指定的鱼
public class CreateFish : MonoBehaviour
{
    public GameObject fishPrefabs;

    public float maxTime;
    private float curTime;

    private void Update()
    {
        curTime += Time.deltaTime;
        if (curTime >= maxTime)
        {
            curTime = 0;
            Create();
        }
    }

    private void Create()
    {
        GameObject go = Instantiate(fishPrefabs);
        go.transform.SetParent(transform,false);

    }
}