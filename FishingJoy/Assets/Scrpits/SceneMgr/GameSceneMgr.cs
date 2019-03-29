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
        gameWind = transform.Find("Canvas/GameWind").GetComponent<MianWind>();
        

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
    private MianWind gameWind;

    //3D
    private Transform gunCreate;//炮的生成父物体/生成点
    private Transform gun3D;//场景炮
    private Transform firePoint;//发射点
    private SpriteRenderer gunIcon;//2d炮图片

    //Net
    private Transform netCreate;


    public void EnterGameScene()//进入游戏场景
    {
        enterGame = true;
        OpenGameWind();
        ResreshUI();

        InitCreateFishPath();
        InitGunCreate();
        InitNetCreate();
    }

    //初始化创建鱼的路径
    private void InitCreateFishPath()
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

    //游戏场景UI
    public void OpenGameWind()
    {
        gameWind.SetWindState();
    }
    public void CloseGameWind()
    {
        gameWind.SetWindState(false);
    }
    public void ResreshUI()
    {
        gameWind.RefreshUI();
    }

    //控制炮
    public void SetGunRotate(float angles)//旋转炮
    {
        SetGunRotate_3D(angles);
    }
    public void SetFire(Vector3 pos)
    {
        SetFire_3D(pos);
    }

    //3D场景物体
    private void InitGunCreate()
    {
        gunCreate = GameObject.Find("GunCreate").transform;
        gun3D = gunCreate.Find("Gun");//3d炮
        firePoint = gun3D.Find("firePoint");
    }
    public void SetGunRotate_3D(float angles)
    {
        gun3D.transform.rotation = Quaternion.Euler(0, 0, angles);
    }
    public void SetFire_3D(Vector3 pos)
    {
        GameObject go = ObjectPool.Instance.Get(PathDefine.BulletPath + "1");

        go.name = PathDefine.BulletPath + "1";
        go.transform.SetParent(gunCreate.transform);
        go.transform.localPosition = firePoint.position;
        go.transform.rotation = gun3D.transform.rotation;

    }
    public Transform GetGunTrans()
    {
        return gun3D.transform;
    }

    //渔网的生成
    private void InitNetCreate()
    {
        netCreate = GameObject.Find("NetCreate").transform;
    }
    public void CreateNet(int gunLv, Vector3 pos)
    {
        string path;
        if (gunLv <= 20)
        {
            path = PathDefine.Net0_20;
        }
        else
        {
            path = PathDefine.NetPath + gunLv;
        }
        Transform net = ObjectPool.Instance.Get(path).transform;
        net.name = path;
        net.SetParent(netCreate);
        net.transform.position = pos;

        net.GetComponent<FishNetBase>().Init();
    }
}