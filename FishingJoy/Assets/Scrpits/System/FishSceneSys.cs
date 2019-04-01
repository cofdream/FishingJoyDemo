using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//渔场业务
public class FishSceneSys : MonoBehaviour
{
    public static FishSceneSys Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {

    }

    public void EnterFishScene()
    {

    }
    public void QuitFishScene()
    {

    }
    public void StartCreateFish()
    {
        
    }
    public void StopCreateFish()
    {

    }
}