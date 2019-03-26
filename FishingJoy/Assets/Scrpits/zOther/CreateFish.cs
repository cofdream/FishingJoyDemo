using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//创建指定类型的鱼 
//每过一段时间 创建出指定的鱼
public class CreateFish : MonoBehaviour
{

    [SerializeField] private string[] fishPath;
    [SerializeField] private Transform parent;
    [SerializeField] private int maxFishCount;//每波鱼的最大生成数量
    [SerializeField] private int curFishCount;//当前生成的鱼的数量
    [SerializeField] private float maxFishTime;//生成每波鱼的秒数
    [SerializeField] private float curFishTime;//当前鱼的生成秒数


    public void Init(string[] fishPath, int maxFishCount, float maxFishTime, Transform parent)
    {
        this.fishPath = fishPath;
        this.maxFishCount = maxFishCount;
        this.maxFishTime = maxFishTime;
        this.parent = parent;
    }

    public void CreateFishUpdate()
    {
        if (curFishCount >= maxFishCount) return;

        curFishTime += Time.deltaTime;
        if (curFishTime >= maxFishTime)
        {
            curFishTime = 0;
            int index = Random.Range(0, fishPath.Length);
            GetFish(fishPath[index]);
            curFishCount++;
        }
    }
    //名字获取鱼
    public GameObject GetFish(string path)
    {
        GameObject fish = ResSvc.Instance.GetPrefabs(path);
        fish = Instantiate(fish);

        fish.transform.SetParent(parent);
        fish.transform.localPosition = Vector3.zero;

        GameSceneMgr.Instance.AddFish(fish);
        return fish;
    }
}