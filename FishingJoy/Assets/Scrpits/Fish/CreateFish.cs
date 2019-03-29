using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//创建指定类型的鱼 
//每过一段时间 创建出指定的鱼
public class CreateFish : MonoBehaviour
{

    [SerializeField] private string[] fishPath;
    [SerializeField] private Transform parent;

    [SerializeField] private float maxFishTime;//生成每波鱼的秒数
    [SerializeField] private float curFishTime;//当前鱼的生成秒数

    public void Init(string[] fishPath, float maxFishTime, Transform parent)
    {
        this.fishPath = fishPath;
        this.maxFishTime = maxFishTime;
        this.parent = parent;
    }
    public void SetPath(string[] fishPath)
    {
        this.fishPath = fishPath;
    }

    public void CreateFishUpdate()
    {
        curFishTime += Time.deltaTime;
        if (curFishTime >= maxFishTime)
        {
            curFishTime = 0;
            int index = Random.Range(0, fishPath.Length);
            GetFish(fishPath[index]);
        }
    }
    //名字获取鱼
    public GameObject GetFish(string path)
    {
        GameObject fish = ObjectPool.Instance.Get(path);
        fish.name = path;

        fish.transform.SetParent(parent);
        fish.transform.localPosition = Vector3.zero;
        fish.transform.localEulerAngles = Vector3.zero;

        fish.GetComponent<FishBase>().Init();
        fish.GetComponent<Move>().SetSpeed(1.2f);
        //设置层级
        GameSceneMgr.Instance.SetFish(fish);

        return fish;
    }
}