/****************************************************
    文件：PlayerController.cs
	作者：cofdream
    邮箱: cofdream@sina.com
    日期：2019-04-10-11:52:19
	功能：角色控制器
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    private bool state;//控制器的激活状态

    private MainSys mainSys;
    private DataSvc dataSvc;
    private ObjectPool pool;
    private SkillControler skillControler;//技能控制器

    private Transform BulletParent;
    private Transform firePoint;
    private Transform fishNetParent;
    private Transform moneyParent;

    private void Start()
    {
        Init();
    }
    void Update()
    {
        if (state == false) return;
#if UNITY_ANDROID || UNITY_IPHONE //移动端判断
             if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
#else
        if (EventSystem.current.IsPointerOverGameObject() == false)
#endif
        {
            if (Input.GetMouseButtonDown(0))
            {
                GunFire();//炮开火
            }
        }

        skillControler.UpdateSkill();//更新技能
    }
    private void Init()//初始化控制器的基本数据
    {
        state = false;
        mainSys = MainSys.Instance;
        dataSvc = DataSvc.Instance;
        pool = ObjectPool.Instance;
        skillControler = GetComponent<SkillControler>();

        //初始化炮的数据
        Transform temp = GameObject.Find("Gun").transform;
        BulletParent = temp.Find("BulletCreate");
        firePoint = temp.Find("FirePoint");
        fishNetParent = GameObject.Find("FishNetCreate").transform;
        moneyParent = GameObject.Find("Money").transform;
    }

    public void SetPlayerCtlState(bool state)//设置玩家控制器的激活状态
    {
        this.state = state;
    }
    public void GunFire()//控制炮开火
    {
        GunRotate();//先旋转炮
        if (dataSvc.pd.Gold < Tools.GetGunMoney(dataSvc.pd.GunLv))
        {
            //生成一个闪动特效提示金币不足
            //TODO
            Debug.Log("金币不足");
        }
        else
        {
            mainSys.SetGunFireUI();//刷新UI炮的开火显示
            CreateBullet();//发射子弹
            DeductFireCost();//扣除开火花费的金币/钻石
        }
    }
    public void GunRotate()//控制炮旋转
    {
        mainSys.SetGunRotateUI();//控制UI炮自行的旋转
        firePoint.transform.rotation = mainSys.GetUIGunRotate();//旋转场景的炮
    }
    public void CreateBullet()//发射子弹
    {
        Transform bullet = pool.Get(PathDefine.BulletPath + dataSvc.pd.GunLv.ToString()).transform;

        bullet.name = PathDefine.BulletPath + dataSvc.pd.GunLv.ToString();
        bullet.SetParent(BulletParent);
        bullet.localPosition = Vector3.zero;
        bullet.rotation = firePoint.transform.rotation;

        Move move = bullet.GetComponent<Move>();
        move.Init(new Vector3(0, 1, 0), 6f);

        //判断技能
        //TODO
        //判断是否处于三连发技能状态
        //if (isUseScatteringSkill)
        //{
        //    SetCreateBullet(new Vector3(0, 0, 15f));
        //    SetCreateBullet(new Vector3(0, 0, -15f));
        //}
    }

    public void DeductFireCost()//扣除开火的花费
    {
        dataSvc.AddGold(-Tools.GetGunMoney(dataSvc.pd.GunLv));
    }
    public void CreateNetFish(Vector3 pos, string fishNetName)//生成渔网
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
    public void AddExp(int fishGold)//增加经验
    {
        dataSvc.AddExp(Tools.GetFishExp(fishGold, dataSvc.pd.GunLv));
    }

    //技能
    public void StartSkillIce()
    {
        skillControler.StartSkillIce();
    }
    public void StartSkillScattering()
    {
        skillControler.StartSkillScattering();
    }
}
