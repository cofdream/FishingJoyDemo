using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//鱼类管理器
public class FishGameMgr : MonoBehaviour
{
    public CreateFish smallFish;
    public CreateFish MediumFish;
    public CreateFish BigFish;
    public CreateFish bossFish;

    private void Init()
    {

        //smallFish = new CreateFish();
        //MediumFish = new CreateFish();
        //初始化鱼群生成的设置
        string[] smallPath = { "Prefabs/Fish/Small/Clownfish", "Prefabs/Fish/Small/Salamander" };
        smallFish.Init(smallPath);
        string[] mediumPath = { "Prefabs/Fish/Medium/Turtle"};
        MediumFish.Init(mediumPath);
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
        MediumFish.CreateFishs();
    }
}