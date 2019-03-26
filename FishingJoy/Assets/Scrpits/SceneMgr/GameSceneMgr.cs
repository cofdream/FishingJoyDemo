using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//鱼类管理器
public class GameSceneMgr : MonoBehaviour
{
    public static GameSceneMgr Instance { get; private set; }

    public GameObject FishCreate { get; private set; } //鱼的生成父物体
    [SerializeField] private List<GameObject> AllFish;
    public bool enterGame;//是否进入游戏场景

    private CreateFish smallFish;
    private CreateFish mediumFish;
    private CreateFish bigFish;
    private CreateFish bossFish;

    private GameWind gameWind;

    public void Init()
    {
        Instance = this;
        enterGame = false;
        AllFish = new List<GameObject>();
        gameWind = transform.Find("Canvas/GameWind").GetComponent<GameWind>();
        gameWind.Init();

        Debug.Log("Init GameSceneMgr");
    }

    public void MyUpdata()
    {
        if (smallFish != null)
        {
            smallFish.CreateFishUpdate();
        }
    }

    public void EnterGameScene()
    {
        OpenGameWind();
        enterGame = true;
        //初始化鱼群生成的设置

        string[] mediumPath = {
            "Prefabs/Fish/Medium/Turtle",
            "Prefabs/Fish/Medium/Forg",
            "Prefabs/Fish/Medium/Lobster",
            "Prefabs/Fish/Medium/SeaHorse",
        };
        string[] bigPath = {
            "Prefabs/Fish/Big/Butterfly",
            "Prefabs/Fish/Big/MantaRay",
            "Prefabs/Fish/Big/Tuna",
        };
        string[] bossPath = {
            "Prefabs/Fish/Boss/Boss1",
            "Prefabs/Fish/Boss/Boss2",
        };

        FishCreate = new GameObject("FishCreate");//生成鱼的父物体
        FishCreate.transform.position = new Vector3(-8, 0, 0);

        SetCreateSmallFish();
    }

    public void AddFish(GameObject fish)
    {
        AllFish.Add(fish);
    }

    private void SetCreateSmallFish() //设置生成小鱼群的类
    {
        smallFish = FishCreate.AddComponent<CreateFish>();
        string[] smallPath = {
            "Prefabs/Fish/Small/Clownfish",
            "Prefabs/Fish/Small/Salamander",
            "Prefabs/Fish/Small/Lobster",
        };
        smallFish.Init(smallPath, 5, 2f, FishCreate.transform);
    }


    public void OpenGameWind()
    {
        gameWind.SetWindState();
    }
    public void CloseGameWind()
    {
        gameWind.SetWindState(false);
    }
    public void SetGunAngles(float angles)
    {
        gameWind.SetGunAngles(angles);
        SetGunAngles_3D(angles);
    }
    public void SetFire(Vector3 pos)
    {
        gameWind.SetFire(pos);
        SetFire_3D(pos);
    }
    public Transform GetGunPos()
    {
        return gameWind.GetGunPos();
    }

    //3D场景物体
    public void SetGunAngles_3D(float angles)
    {

    }
    public void SetFire_3D(Vector3 pos)
    {
        GameObject go = ResSvc.Instance.GetPrefabs(PathDefine.BulletPath + "1");

        go = Instantiate(go);
        
    }
}