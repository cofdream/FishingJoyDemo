/****************************************************
    文件：CreateFish_04.cs
	作者：cofdream
    邮箱: cofdream@sina.com
    日期：2019-04-10-13:57:57
	功能：创建指定类型的鱼群
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateFish_04 : CreateFishBase
{
    public string[] fishPath;
    public Vector3[] fishPos;

    public float[] rotates;

    public float maxTime;


    void Update()
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
            go.transform.rotation = Quaternion.Euler(0, 0, rotates[i]);

            go.GetComponentInChildren<SpriteRenderer>().sortingOrder = FishSceneSys.Instance.GetFishOrderLayer();

            FishBase fishBase = go.GetComponent<FishBase>();
            fishBase.Init();

            Move move = go.GetComponent<Move>();
            move.Init(moveDirection, fishSpeed);
        }

    }
}
