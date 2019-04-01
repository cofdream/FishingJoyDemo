using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//主游戏场景业务系统
public class MainSys : MonoBehaviour
{
    public static MainSys Instance { get; private set; }

    private MainWind mainWind;
    private ObjectPool pool;
    private DataSvc dataSvc;
    private bool isFire = false;

    private Transform BulletParent;
    private Transform firePoint;

    public void InitSys()
    {
        Instance = this;
        mainWind = transform.Find("Canvas/MianWind").GetComponent<MainWind>();

        Debug.Log("Init MainSys Done.");
    }

    private void Update()
    {
        if (isFire)
        {
#if UNITY_ANDROID || UNITY_IPHONE //移动端判断
             if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
#else
            if (EventSystem.current.IsPointerOverGameObject())
#endif
            {
                return;
            }
            if (Input.GetMouseButtonDown(0))
            {
                SetGunRotate();
                SetGunFire();
            }
        }

    }

    public void EnterGame()
    {
        isFire = true;
        OpenMainWind();
        EnterCreateFish();
        pool = ObjectPool.Instance;
        dataSvc = DataSvc.Instance;
        Transform temp = GameObject.Find("Gun").transform;
        BulletParent = temp.Find("BulletCreate");
        firePoint = temp.Find("FirePoint");
    }
    public void QuitGame()
    {
        isFire = false;
        CloseMainWind();
        QuitCreateFish();
        pool = null;
        dataSvc = null;
        BulletParent = null;
        firePoint = null;
    }

    public void SetGunRotate()//炮的旋转
    {
        mainWind.SetGunRotate();
        firePoint.transform.rotation = mainWind.gun2DTrans.rotation;//旋转场景的炮
    }
    public void SetGunFire()//炮的开火
    {
        mainWind.SetGunFire();
        CreateBullet();
        AddEnergy();//增加能量
    }
    private void CreateBullet()
    {
        Transform bullet = pool.Get(PathDefine.BulletPath + dataSvc.pd.GunLv.ToString()).transform;

        bullet.name = PathDefine.BulletPath + dataSvc.pd.GunLv.ToString();
        bullet.SetParent(BulletParent);
        bullet.localPosition = Vector3.zero;
        bullet.rotation = firePoint.transform.rotation;
    }

    //MainWind
    public void OpenMainWind()
    {
        mainWind.SetWindState();
    }
    public void CloseMainWind()
    {
        mainWind.SetWindState(false);
    }
    public void AddEnergy()
    {
        //增加玩家的能量
        //TODO
    }

    //生成鱼
    public void EnterCreateFish()
    {
        FishSceneSys.Instance.EnterFishScene();
    }
    public void QuitCreateFish()
    {
        FishSceneSys.Instance.QuitFishScene();
    }

}