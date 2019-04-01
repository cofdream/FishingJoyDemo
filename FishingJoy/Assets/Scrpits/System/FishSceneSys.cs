using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//渔场业务
public class FishSceneSys : MonoBehaviour
{
    public static FishSceneSys Instance { get; private set; }

    private CreateFishBase[] allCreateFishing;
    private int allCreateCount;
    private void Awake()
    {
        Instance = this;
        allCreateFishing = GetComponents<CreateFishBase>();
        allCreateCount = allCreateFishing.Length;
        for (int i = 0; i < allCreateCount; i++)
        {
            allCreateFishing[i].Init();//调用鱼群生成的初始化方法 （部分鱼需要一开始就生成 有些不需要）
        }
    }

    private void Update()
    {

    }

    public void EnterFishScene()
    {
        SetAllCreateFishingState(true);
    }
    public void QuitFishScene()
    {
        SetAllCreateFishingState(false);
    }

    public void StartCreateFish()
    {
        
    }
    public void StopCreateFish()
    {

    }
    
    private void SetAllCreateFishingState(bool state)//设置鱼群的创建状态
    {
        for (int i = 0; i < allCreateCount; i++)
        {
            allCreateFishing[i].IsCreate = state;
        }
    }
}