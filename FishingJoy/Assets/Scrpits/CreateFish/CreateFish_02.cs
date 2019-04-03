using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//创建指定类型的鱼 
//每过一段时间 创建出指定的鱼
public class CreateFish_02 : CreateFishBase
{
    public string[] fishPath;
    public Vector3[] fishPos;

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
        for (int i = 0; i < fishPath.Length; i++)
        {
            base.CreateFish();

            GameObject go = ObjectPool.Instance.Get(rootPath + fishPath[i]);
            go.name = rootPath + fishPath[i];
            go.transform.SetParent(transform);
            go.transform.localPosition = fishPos[i];
            go.transform.rotation = Quaternion.Euler(0, 0, rotate);

            go.GetComponentInChildren<SpriteRenderer>().sortingOrder = FishSceneSys.Instance.GetFishOrderLayer();

            FishBase fishBase = go.GetComponent<FishBase>();
            fishBase.Init();

            Move move = go.GetComponent<Move>();
            move.Init(moveDirection, fishSpeed);
        }

    }
}