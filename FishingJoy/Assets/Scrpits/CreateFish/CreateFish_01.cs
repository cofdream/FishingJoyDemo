using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//创建指定类型的鱼 
//每过一段时间 创建出指定的鱼
public class CreateFish_01 : CreateFishBase
{
    public string fishPath;
    public Vector3 fishPos;

    public Vector3 moveDirection;
    public float fishSpeed;
    public float rotate;

    public float maxTime;
    private float curTime;


    private void Update()
    {
        if (IsCreate == false) return;

        curTime += Time.deltaTime;
        if (curTime >= maxTime)
        {
            curTime = 0;
            CreateFish();
        }
    }

    public override void Init()
    {
        base.Init();
        CreateFish();
    }

    public override void CreateFish()
    {
        base.CreateFish();

        GameObject go = ObjectPool.Instance.Get(rootPath + fishPath);
        go.name = rootPath + fishPath;
        go.transform.SetParent(transform);
        go.transform.localPosition = fishPos;
        go.transform.rotation = Quaternion.Euler(0, 0, rotate);

        go.GetComponentInChildren<SpriteRenderer>().sortingOrder = FishSceneSys.Instance.GetFishOrderLayer();

        FishBase fishBase = go.GetComponent<FishBase>();
        fishBase.Init();


        Move move = go.GetComponent<Move>();
        move.Init(moveDirection, fishSpeed);
    }
}