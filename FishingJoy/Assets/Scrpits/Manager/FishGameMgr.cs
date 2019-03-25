using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//鱼类管理器
public class FishGameMgr : MonoBehaviour
{
    public CreateFish smallFish;
    public CreateFish mediumFish;
    public CreateFish bigFish;
    public CreateFish bossFish;

    private void Init()
    {
        //smallFish = new CreateFish();
        //MediumFish = new CreateFish();
        //初始化鱼群生成的设置
        string[] smallPath = {
            "Prefabs/Fish/Small/Clownfish",
            "Prefabs/Fish/Small/Salamander",
            "Prefabs/Fish/Small/Lobster",
        };
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

        smallFish.Init(smallPath);
        mediumFish.Init(mediumPath);
        bigFish.Init(bigPath);
        bossFish.Init(bossPath);
    }
    private void Awake()
    {
        Init();
    }

    void Start()
    {
        
    }

    void Update()
    {
        smallFish.CreateFishs();
        mediumFish.CreateFishs();
        bigFish.CreateFishs();
        bossFish.CreateFishs();
    }
}