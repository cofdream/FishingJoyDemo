/****************************************************
    文件：PlayerController.cs
	作者：cofdream
    邮箱: cofdream@sina.com
    日期：2019-04-10-11:52:19
	功能：角色控制器
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    private bool state;//控制器的激活状态

    private MainSys mainSys;
    private DataSvc dataSvc;
    private ObjectPool pool;
    private SkillControler skillControler;//技能控制器

    private bool isFire;//是否可以开火
    private float curFireTime;//当前开火间隔
    private float maxFireTime;//最大开火间隔
    private float chageValue;

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

        GunFireTimer();//开火计时
        skillControler.UpdateSkill();//更新技能

        ChageGunLv();//切换炮的等级

        if (Input.GetMouseButtonDown(0))
        {

#if UNITY_ANDROID || UNITY_IPHONE //移动端判断
             if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) == false)

#else
           
            if (EventSystem.current.IsPointerOverGameObject() == false)
#endif
            {
                GunFire();//炮开火
            }
        }

    }
    private void Init()//初始化控制器的基本数据
    {
        state = false;
        isFire = false;
        maxFireTime = Constant.MaxFireTime;
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
        if (isFire == false) return;

        GunRotate();//先旋转炮

        if (dataSvc.Pd.Gold < dataSvc.GetGunMoney())
        {
            NotMoneyAnime(); //生成一个闪动特效提示金币不足
        }
        else
        {
            isFire = false;//关闭开火
            mainSys.SetGunFireUI();//刷新UI炮的开火显示
            GunBullet();//发射子弹
            DeductFireCost();//扣除开火花费的金币/钻石
        }
    }
    public void GunRotate()//控制炮旋转
    {
        mainSys.SetGunRotateUI();//控制UI炮自行的旋转
        firePoint.transform.rotation = mainSys.GetUIGunRotate();//旋转场景的炮
    }

    private void ChageGunLv()//切换炮的等级
    {
        chageValue = Input.GetAxis("Mouse ScrollWheel");
        if (chageValue > 0)
        {
            mainSys.AddGunLv(1);
        }
        else if (chageValue < 0)
        {
            mainSys.AddGunLv(-1);
        }
    }

    private void GunFireTimer()//开火计时
    {
        curFireTime += Time.deltaTime;
        if (curFireTime >= maxFireTime)
        {
            curFireTime = 0;
            isFire = true;
        }
    }

    private void GunBullet()//发射子弹
    {
        CreateBullet(Vector3.zero, 6f);
        //判断是否处于三连发技能状态
        if (skillControler.IsUseScatteringSkill)
        {
            CreateBullet(new Vector3(0, 0, 15f), 6f);
            CreateBullet(new Vector3(0, 0, -15f), 6f);
        }
    }
    private void CreateBullet(Vector3 rotate, float speed)//创建子弹
    {
        Transform bullet = pool.Get(PathDefine.BulletPath + dataSvc.Pd.GunLv.ToString()).transform;

        bullet.name = PathDefine.BulletPath + dataSvc.Pd.GunLv.ToString();
        bullet.SetParent(BulletParent);
        bullet.localPosition = Vector3.zero;
        bullet.rotation = firePoint.transform.rotation;
        bullet.Rotate(rotate);//散射需要旋转一下


        BulletBase bulletbase = bullet.GetComponent<BulletBase>();
        bulletbase.Init();
        bulletbase.InitBullet(new Vector3(0, 1, 0), speed);
    }

    public void DeductFireCost()//扣除开火的花费
    {
        dataSvc.AddGold(-dataSvc.GetGunMoney());
    }
    public void CreateNetFish(Vector3 pos, string fishNetName)//生成渔网
    {
        int gunLv = dataSvc.Pd.GunLv;
        GameObject netFish = pool.Get(fishNetName + gunLv);

        netFish.name = fishNetName + gunLv;
        netFish.transform.SetParent(fishNetParent);
        netFish.transform.position = pos;

        FishNetBase fishNet = netFish.GetComponent<FishNetBase>();
        fishNet.Init();
        fishNet.gunMoney = dataSvc.GetGunMoney();
    }
    public void AddExp(int fishGold)//增加经验
    {
        dataSvc.AddExp(dataSvc.GetFishExp(fishGold));
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

    //资金不足的提示动画
    public void NotMoneyAnime()
    {
        MainSys.Instance.NotMoneyAnime();
    }
}
