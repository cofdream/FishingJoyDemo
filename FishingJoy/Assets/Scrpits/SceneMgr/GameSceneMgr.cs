using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//游戏场景管理器
public class GameSceneMgr : MonoBehaviour
{
    public static GameSceneMgr Instance { get; private set; }
    public void Init()
    {
        Instance = this;
        enterGame = false;
        curLayer = 0;
        gameWind = transform.Find("Canvas/GameWind").GetComponent<GameWind>();
        gameWind.Init();

        Debug.Log("Init GameSceneMgr");
    }
    public void MyUpdata()
    {
        //开始持续生成鱼
        if (smallFish != null)
        {
            smallFish.CreateFishUpdate();
        }
        if (mediumFish != null)
        {
            mediumFish.CreateFishUpdate();
        }
        if (bigFish != null)
        {
            bigFish.CreateFishUpdate();
        }
        if (bossFish != null)
        {
            bossFish.CreateFishUpdate();
        }
    }

    [HideInInspector] public bool enterGame;//是否进入游戏场景

    private short curLayer;//当前鱼的层级
    private CreateFish smallFish;
    private CreateFish mediumFish;
    private CreateFish bigFish;
    private CreateFish bossFish;
    //UI
    private GameWind gameWind;

    //3D
    private GameObject GunCreate;//炮的生成父物体/生成点
    private Transform gun3D;//场景炮
    private Transform firePoint;//发射点
    private SpriteRenderer gunIcon;//2d炮图片

    public void EnterGameScene()//进入游戏场景
    {
        enterGame = true;
        OpenGameWind();

        SetCreatePath();

        GunCreate = new GameObject("GunCreate");//生成炮的父物体
        GunCreate.transform.position = new Vector3(0, 0, 0);
        gun3D = GameObject.Find("Gun").transform;//生成3d炮
        firePoint = gun3D.transform.Find("firePoint");
    }
    //初始化创建鱼的路径
    private void SetCreatePath()
    {
        Transform temp = GameObject.Find("CreateFishPoints").transform;
        smallFish = temp.Find("smallFishCreate").GetComponent<CreateFish>();
        mediumFish = temp.Find("mediumFishCreate").GetComponent<CreateFish>();
        bigFish = temp.Find("bigFishCreate").GetComponent<CreateFish>();
        bossFish = temp.Find("bossFishCreate").GetComponent<CreateFish>();
        smallFish.SetPath(new string[] {
            PathDefine.Clownfish, PathDefine.Salamander
        });
        mediumFish.SetPath(new string[] {
            PathDefine.Turtle, PathDefine.Forg, PathDefine.Lobster, PathDefine.SeaHorse
        });
        bigFish.SetPath(new string[] {
            PathDefine.Butterfly, PathDefine.MantaRay,PathDefine.Tuna
        });
        bossFish.SetPath(new string[] {
            PathDefine.Boss1, PathDefine.Boss2
        });

    }
    public void SetFish(GameObject fish)
    {
        SpriteRenderer sp = fish.GetComponentInChildren<SpriteRenderer>();
        sp.sortingOrder = curLayer++;
    }

    //控制炮
    public void SetGunAngles(float angles)//旋转炮
    {
        gameWind.SetGunAngles(angles);
        SetGunAngles_3D(angles);
    }
    public void SetFire(Vector3 pos)
    {
        gameWind.SetFire(pos);
        SetFire_3D(pos);
    }

    //游戏场景UI
    public void OpenGameWind()
    {
        gameWind.SetWindState();
    }
    public void CloseGameWind()
    {
        gameWind.SetWindState(false);
    }
    public Transform GetGunTrans()
    {
        return gameWind.GetGunTrans();
    }

    //3D场景物体
    public void SetGunAngles_3D(float angles)
    {
        gun3D.transform.rotation = Quaternion.Euler(0, 0, angles);
    }
    public void SetFire_3D(Vector3 pos)
    {
        GameObject go = ObjectPool.Instance.Get(PathDefine.BulletPath + "1");

        go.name = PathDefine.BulletPath + "1";
        go.transform.SetParent(GunCreate.transform);
        go.transform.position = firePoint.position;
        go.transform.rotation = gun3D.transform.rotation;
    }
}