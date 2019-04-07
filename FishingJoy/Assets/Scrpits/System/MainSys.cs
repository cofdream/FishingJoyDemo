
using UnityEngine;
using UnityEngine.EventSystems;

//主游戏场景业务系统
public class MainSys : MonoBehaviour
{
    public static MainSys Instance { get; private set; }

    private MainWind mainWind;
    private TipsWind tipsWind;
    private ObjectPool pool;
    private DataSvc dataSvc;

    #region 炮
    private bool isFire = false;
    private Transform BulletParent;
    private Transform firePoint;
    private Transform fishNetParent;
    private Transform moneyParent;
    #endregion

    #region 海浪
    private float seneTime;
    private SpriteRenderer mapbg;
    #endregion

    #region Skill
    private SpriteRenderer mapbgef;
    //散射
    private bool isCanUseScatteringSkill = false;//是否可以使用技能
    private bool isUseScatteringSkill = false;
    private float curScatteringTime = 0f;
    private float maxUseScatteringSkillTime = 5f;
    private float maxCDScatteringSkillTime = 20f;
    //冰冻
    private bool isCanUseIceSkill = false;
    private bool isUseIceSkill = false;
    private float curIceSkillTime = 0f;
    private float maxUseIceSkillTime = 2f;
    private float maxCDIceSkillTime = 3f;
    #endregion

    public void InitSys()
    {
        Instance = this;
        dataSvc = DataSvc.Instance;
        mainWind = transform.Find("Canvas/MianWind").GetComponent<MainWind>();
        tipsWind = transform.Find("Canvas/TipsWind").GetComponent<TipsWind>();
        tipsWind.Init();

        Debug.Log("Init MainSys Done.");
    }

    private void Update()
    {
        SetFire();//设置枪的开火旋转技能等
    }

    public void EnterGame()//进入游戏场景
    {
        isFire = true;
        OpenMainWind();
        StartCreateFish();
        pool = ObjectPool.Instance;
        InitGunData();
        InitMap2D();
        InitEfMapBg();
        SetMapBg();//设置地图背景
        PlayeBgAudio();//播放背景音乐
        //初始化技能数据
        InitScatteringSkill();
        InitIceSkill();
    }
    public void ExitGame()//退出游戏场景
    {
        isFire = false;
        CloseMainWind();
        QuitCreateFish();
        //BUG 金币在退出场景时候还能显示 等待修复...（目前没有什么好的想法）
    }

    //炮
    private void InitGunData()//初始化炮的数据
    {
        if (BulletParent == null)//只初始化一次
        {
            Transform temp = GameObject.Find("Gun").transform;
            BulletParent = temp.Find("BulletCreate");
            firePoint = temp.Find("FirePoint");
            fishNetParent = GameObject.Find("FishNetCreate").transform;
            moneyParent = GameObject.Find("Money").transform;
        }
    }
    private void SetFire()
    {
        if (isFire)
        {
#if UNITY_ANDROID || UNITY_IPHONE //移动端判断
             if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
#else
            if (EventSystem.current.IsPointerOverGameObject())
#endif
            {
                //return;
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    SetGunRotate();
                    SetGunFire();
                }
            }

            //ChangFishScene();//换场计时

            //计时技能
            Skill_Scattering_Timer();
            SkillIce_Timer();
        }
    }
    private void SetGunRotate()//炮的旋转
    {
        mainWind.SetGunRotate();
        firePoint.transform.rotation = mainWind.Gun2DTrans.rotation;//旋转场景的炮
    }
    private void SetGunFire()//炮的开火
    {
        if (dataSvc.pd.Gold < Tools.GetGunMoney(dataSvc.pd.GunLv))
        {
            //生成一个闪动特效提示金币不足
            //TODO
            Debug.Log("金币不足");
            return;
        }
        mainWind.SetGunFire();
        CreateBullet();//生成子弹
        AddEnergy();//增加能量
        DeductFireCost();//扣除开火花费的金币/钻石
    }
    private void CreateBullet()
    {
        SetCreateBullet(new Vector3(0, 0, 0));

        //判断是否处于三连发技能状态
        if (isUseScatteringSkill)
        {
            SetCreateBullet(new Vector3(0, 0, 15f));
            SetCreateBullet(new Vector3(0, 0, -15f));
        }

    }
    private void SetCreateBullet(Vector3 euler)
    {
        Transform bullet = pool.Get(PathDefine.BulletPath + dataSvc.pd.GunLv.ToString()).transform;

        bullet.name = PathDefine.BulletPath + dataSvc.pd.GunLv.ToString();
        bullet.SetParent(BulletParent);
        bullet.localPosition = Vector3.zero;
        bullet.rotation = firePoint.transform.rotation;
        bullet.Rotate(euler);

        Move move = bullet.GetComponent<Move>();
        move.Init(new Vector3(0, 1, 0), 6f);
    }
    private void DeductFireCost()//扣除开火的花费
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
    public void GetExp(int fishGold)//增加经验
    {
        dataSvc.AddExp(Tools.GetFishExp(fishGold, dataSvc.pd.GunLv));
        mainWind.RefreshUI();
    }

    //海浪换场
    private void InitMap2D()
    {
        if (mapbg == null)
        {
            mapbg = GameObject.Find("mapbg").GetComponent<SpriteRenderer>();
        }
    }
    private void ChangFishScene() //换场
    {
        //判断是否可以换场 可以使用定时换场/等级换场
        seneTime += Time.deltaTime;
        if (seneTime >= Constant.ChangeFishScene)
        {
            seneTime = 0;
            isFire = false;//关闭开火
            //开启换场特效  //清空场景鱼群/调用鱼群的逃跑方法
            GameObject seaWave = pool.Get(PathDefine.SeaWave);
            seaWave.name = PathDefine.SeaWave;
            seaWave.transform.position = Vector3.zero;
            seaWave.GetComponent<MoveTo>().SetMove(new Vector3(-17, 0, 0), 0.3f, () =>
            {
                isFire = true;//开启开火
                pool.Put(seaWave.name, seaWave);
                //重新生成鱼群
                FishSceneSys.Instance.SetAllCreateFishingState(true);
                //切换场景的背景
                dataSvc.AddFishSceneLv(1);
                SetMapBg();
                //切换场景背景音乐
                PlayeBgAudio();
            });
            //关闭鱼的生成
            FishSceneSys.Instance.SetAllCreateFishingState(false);
        }
    }
    private void SetMapBg()//设置地图背景
    {
        mapbg.sprite = ResSvc.Instance.LoadSprite(PathDefine.MapBg + dataSvc.pd.FishSceneLv.ToString());
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
    public void StartCreateFish()
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
            go.transform.SetParent(moneyParent);
            go.transform.position = pos.position;
            go.name = PathDefine.Gold;
            MoveTargetPos mo = go.GetComponent<MoveTargetPos>();
            mo.Init(mainWind.GetGoldPos(), 50f);

            dataSvc.AddGold(gold);

            AudioSvc.Instance.PlayUIAudio(PathDefine.EfGetGold, false, true);
        }
        if (diamond > 0)
        {
            go = pool.Get(PathDefine.Diamond);
            go.transform.SetParent(moneyParent);
            go.transform.position = pos.position;
            go.name = PathDefine.Diamond;
            MoveTargetPos mo = go.GetComponent<MoveTargetPos>();
            mo.Init(mainWind.GetDiamondPos(), 50f);

            dataSvc.AddDiamond(diamond);

            AudioSvc.Instance.PlayUIAudio(PathDefine.EfGetDiamond, false, true);
        }

    }

    //背景音乐
    private void PlayeBgAudio()
    {
        string path;
        switch (dataSvc.pd.FishSceneLv)
        {
            case 1:
                path = PathDefine.BgLv1;
                break;
            case 2:
                path = PathDefine.BgLv2;
                break;
            case 3:
                path = PathDefine.BgLv3;
                break;
            default:
                path = PathDefine.BgLv1;
                break;
        }

        AudioSvc.Instance.PlayBgAudio(path);
    }

    #region 技能
    public void OnClickIce()
    {
        //打开冰冻背景
        if (isCanUseIceSkill && isUseIceSkill == false)
        {
            SetEfMapBg("_1");//设置冰冻背景
            curScatteringTime = maxUseIceSkillTime;
            isUseIceSkill = true;
            FishSceneSys.Instance.SetIceSkillState(true);//设置鱼群为冰冻状态
            FishSceneSys.Instance.IceStateStopMove(maxUseIceSkillTime);//暂停鱼的移动
            SetEfMapBgState(true);
;        }
    }
    private void InitIceSkill()//初始化冰冻技能数据
    {
        isCanUseIceSkill = false;
        isUseIceSkill = false;
        curIceSkillTime = 0f;    //切换场景以后 是否保存cd进度？暂时不保存
    }
    private void SkillIce_Timer()//技能计时器
    {
        //增加计时功能 
        if (isCanUseIceSkill)
        {
            if (isUseIceSkill)
            {
                curIceSkillTime -= Time.deltaTime;//倒计时
                if (curIceSkillTime <= 0f)
                {
                    //技能使用时间结束
                    curIceSkillTime = 0;
                    //设置不能使用技能 
                    isCanUseIceSkill = false;
                    //使用技能结束
                    isUseIceSkill = false;
                    FishSceneSys.Instance.SetIceSkillState(false);//设置鱼群取消冰冻状态
                    SetEfMapBgState(false);
                }
                mainWind.SetIceSkillCD(curIceSkillTime / maxUseIceSkillTime);
            }
        }
        else
        {
            curIceSkillTime += Time.deltaTime;
            if (curIceSkillTime >= maxCDIceSkillTime)
            {
                //可以使用技能了
                isCanUseIceSkill = true;
                mainWind.SetIceSkillMask(false);//关闭黑色遮罩
            }
            else
            {
                mainWind.SetIceSkillMask(true);//打开黑色遮罩
            }
            mainWind.SetIceSkillCD(curIceSkillTime / maxCDIceSkillTime);
        }
    }

    public void OnClickFire()
    {
        //打开燃烧背景
        //TODO
    }

    public void OnClickScattering()
    {
        if (isCanUseScatteringSkill && isUseScatteringSkill == false)
        {
            curScatteringTime = maxUseScatteringSkillTime;
            isUseScatteringSkill = true;
        }
    }
    private void InitScatteringSkill()
    {
        isCanUseScatteringSkill = false;
        isUseScatteringSkill = false;
        curScatteringTime = 0f;//是否保存cd进度？暂时不保存
    }
    private void Skill_Scattering_Timer()//技能计时器
    {
        //散射计时
        //一开始 不能使用技能  进行技能cd的充能
        if (isCanUseScatteringSkill)
        {
            if (isUseScatteringSkill)//是否激活了技能的使用
            {
                //倒计时
                curScatteringTime -= Time.deltaTime;
                if (curScatteringTime <= 0f)
                {
                    //技能使用时间结束
                    curScatteringTime = 0;
                    //设置不能使用技能 
                    isCanUseScatteringSkill = false;
                    //使用技能结束
                    isUseScatteringSkill = false;
                }
                //设置技能使用的进度显示
                mainWind.SetScatteringSkillCD(curScatteringTime / maxUseScatteringSkillTime);
            }
        }
        else
        {
            curScatteringTime += Time.deltaTime;
            if (curScatteringTime >= maxCDScatteringSkillTime)
            {
                //技能已经充能好了,可以使用
                isCanUseScatteringSkill = true;
                mainWind.SetScatteringSkillMask(false);//关闭黑色遮罩
            }
            else
            {
                mainWind.SetScatteringSkillMask(true);
            }
            //设置技能冷却的进度显示
            mainWind.SetScatteringSkillCD(curScatteringTime / maxCDScatteringSkillTime);
        }
    }

    private void InitEfMapBg()//初始化背景特效
    {
        if (mapbgef == null)
        {
            mapbgef = GameObject.Find("mapbgef").GetComponent<SpriteRenderer>();
        }
    }
    private void SetEfMapBg(string path)//设置特效背景
    {
        //直接显示/使用特效渐变
        mapbgef.sprite = ResSvc.Instance.LoadSprite(PathDefine.MapBg + dataSvc.pd.FishSceneLv.ToString() + path);
    }
    private void SetEfMapBgState(bool state)
    {
        mapbgef.gameObject.SetActive(state);
    }

    #endregion
}