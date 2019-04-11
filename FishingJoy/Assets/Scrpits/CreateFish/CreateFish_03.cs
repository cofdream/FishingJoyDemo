using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//创建指定类型的鱼 
//每过一段时间 创建出指定的鱼
public class CreateFish_03 : CreateFishBase
{
    public string[] fishPath;
    public Vector3[] fishPos;

    public float maxTime;
    public float rotate;

    private void Update()
    {
        if (isCreate == false) return;
        if (iceState) return;

        curTime += Time.deltaTime;
        if (curTime >= maxTime)
        {
            curTime = 0;
            CreateFish();
        }
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

            FishRotate fihsRotate = go.GetComponent<FishRotate>();
            if (fihsRotate == null)
            {
                fihsRotate = go.AddComponent<FishRotate>();
            }
            fihsRotate.Init(0, 10, 2f, 0.5f);
        }

    }
}