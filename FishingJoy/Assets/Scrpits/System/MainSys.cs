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

    private Transform fishNetParent;

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

        fishNetParent = GameObject.Find("FishNetCreate").transform;
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
        if (dataSvc.pd.Gold < Tools.GetGunMoney(dataSvc.pd.GunLv))
        {
            //生成一个闪动特效提示金币不足
            //TODO
            Debug.Log("金币不足");
            return;
        }
        mainWind.SetGunFire();
        CreateBullet();
        AddEnergy();//增加能量
        DeductFireCost();//扣除开火花费的金币/钻石
    }
    private void CreateBullet()
    {
        Transform bullet = pool.Get(PathDefine.BulletPath + dataSvc.pd.GunLv.ToString()).transform;

        bullet.name = PathDefine.BulletPath + dataSvc.pd.GunLv.ToString();
        bullet.SetParent(BulletParent);
        bullet.localPosition = Vector3.zero;
        bullet.rotation = firePoint.transform.rotation;

        Move move = bullet.GetComponent<Move>();
        move.Init(new Vector3(0, 1, 0), 6f);
    }
    private void DeductFireCost()
    {
        dataSvc.AddGold(-Tools.GetGunMoney(dataSvc.pd.GunLv));
        mainWind.RefreshUI();
    }
    public void CreateNetFish(Vector3 pos, string fishNetName)
    {
        int gunLv = dataSvc.pd.GunLv;
        GameObject netFish = pool.Get(fishNetName + gunLv);

        netFish.name = fishNetName + gunLv;
        netFish.transform.SetParent(fishNetParent);
        netFish.transform.position = pos;

        FishNetBase fishNet = netFish.GetComponent<FishNetBase>();
        fishNet.Init();
        fishNet.gunMoney = Tools.GetGunMoney(gunLv);
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
    public void CreateGoldAndDimand(Transform pos, int gold, int diamond)
    {
        GameObject go;
        if (gold > 0)
        {
            go = pool.Get(PathDefine.Gold);
            go.transform.position = pos.position;
            go.name = PathDefine.Gold;
            MoveTargetPos mo = go.GetComponent<MoveTargetPos>();
            mo.Init(mainWind.GetGoldPos(), 50f);

            dataSvc.AddGold(gold);
        }
        if (diamond > 0)
        {
            go = pool.Get(PathDefine.Diamond);
            go.transform.position = pos.position;
            go.name = PathDefine.Diamond;
            MoveTargetPos mo = go.GetComponent<MoveTargetPos>();
            mo.Init(mainWind.GetDiamondPos(), 50f);

            dataSvc.AddDiamond(diamond);
        }

    }
}