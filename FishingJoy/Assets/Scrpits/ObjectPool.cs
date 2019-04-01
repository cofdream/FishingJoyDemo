  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//对象池 鱼/子弹/网
public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance { get; private set; }
    public Dictionary<string, Queue<GameObject>> dicPool;
    private Transform pool;
    private ResSvc resSvc;

    public void Init()
    {
        Instance = this;
        resSvc = ResSvc.Instance;
        dicPool = new Dictionary<string, Queue<GameObject>>();
        pool = new GameObject("pool").transform;
        pool.SetParent(transform);
    }

    public GameObject Get(string path)
    {
        Queue<GameObject> temp;
        GameObject go;
        if (dicPool.TryGetValue(path, out temp) == false)
        {
            temp = new Queue<GameObject>();
            dicPool.Add(path, temp);
        }
        if (temp.Count <= 0)
        {
            go = resSvc.LoadPrefab(path);
        }
        else
        {
            go = temp.Dequeue();
        }
        go.SetActive(true);
        return go;
    }

    public void Put(string path,GameObject go)
    {
        Queue<GameObject> temp;
        if (dicPool.TryGetValue(path,out temp))
        {
            go.SetActive(false);
            go.transform.SetParent(pool);
            temp.Enqueue(go);
        }
    }
}