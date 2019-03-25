using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//创建鱼
public class CreateFish : MonoBehaviour
{

    public string[] fishPath;
    public int maxFishCount;//每波鱼的最大生成数量
    public float maxFishTime;//每波鱼的生成秒数
    public float curFishTime;//当前鱼的生成秒数

    public void Init(string[] fishPath)
    {
        this.fishPath = fishPath;
    }

    public void CreateFishs()
    {
        curFishTime += Time.deltaTime;
        if (curFishTime >= maxFishTime)
        {
            curFishTime = 0;
            int index = Random.Range(0, fishPath.Length);
            GameObject go = ResSvc.Instance.GetPrefabs(fishPath[index]);
            go = Instantiate(go);
            go.transform.position = Vector3.zero;
            Move move = go.GetComponent<Move>();
            move.Init(new Vector3(1, 0, 0), 0.6f);
            move.StartMove();
        }
    }
}