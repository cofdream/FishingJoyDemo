
using System;
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

    private PlayerController playerController;//玩家控制器

    #region 炮
    private bool isPlayState = false;
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
    private Ef_FadeAway _mapbgef_FA;
    //散射
    private bool isCanUseScatteringSkill;//是否可以使用技能

    private bool isUseScatteringSkill;
    private float curScatteringTime;
    private float maxUseScatteringSkillTime;
    private float maxCDScatteringSkillTime;
    //冰冻
    private bool isCanUseIceSkill;
    private bool isUseIceSkill;
    private float curIceSkillTime;
    private float maxUseIceSkillTime;
    private float maxCDIceSkillTime;
    #endregion

    public void InitSys()
    {
        Instance = this;
        dataSvc = DataSvc.Instance;
        mainWind = transform.Find("Canvas/MianWind").GetComponent<MainWind>();
        tipsWind = transform.Find("Canvas/TipsWind").GetComponent<TipsWind>();

        Debug.Log("Init MainSys Done.");
    }

    private void Update()
    {

    }

    public void EnterGame()//进入游戏场景
    {
        isPlayState = true;
        OpenMainWind();
        StartCreateFish();
        pool = ObjectPool.Instance;
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        playerController.SetPlayerCtlState(true);

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
        isPlayState = false;
        CloseMainWind();
        QuitCreateFish();//停止鱼群创建和清除鱼群
        playerController.SetPlayerCtlState(false);
        //清空子弹
        //清除渔网
        //隐藏特效背景
        //清除金币和钻石

        //BUG 金币在退出场景时候还能显示 等待修复...（目前没有什么好的想法）
    }

    //炮
    private void SetFire()
    {
        if (isPlayState)
        {
            ChangFishScene();//换场计时

            //计时技能
            Skill_Scattering_Timer();
            SkillIce_Timer();
        }
    }
    public Quaternion GetUIGunRotate()
    {
        return mainWind.GetGunRotate();
    }
    public void CreateNetFish(Vector3 pos, string fishNetName)//创建渔网
    {
        playerController.CreateNetFish(pos, fishNetName);
    }
    public void AddExp(int fishGold)
    {
        playerController.AddExp(fishGold);
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
            isPlayState = false;//关闭开火
            //开启换场特效  //清空场景鱼群/调用鱼群的逃跑方法
            GameObject seaWave = pool.Get(PathDefine.SeaWave);
            seaWave.name = PathDefine.SeaWave;
            seaWave.transform.position = Vector3.zero;
            seaWave.GetComponent<Seawave>().StartSeawave(() =>
            {
                isPlayState = true;//开启开火
                pool.Put(seaWave.name, seaWave);
                //重新生成鱼群
                FishSceneSys.Instance.SetAllCreateFishingState(true);
                //切换场景的背景
                dataSvc.AddFishSceneLv(1);
                SetMapBg();
                //切换场景背景音乐
                PlayeBgAudio();
                pool.Put(PathDefine.SeaWave, seaWave);
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
    public void RefreshExpAndLv()//刷新等级和经验
    {
        mainWind.RefreshExpAndLv();
    }
    public void RefreshMoney()//刷新钱的显示
    {
        mainWind.RefreshMoney();
    }
    public void RefreshGunUI()//刷新炮的UI图片
    {
        mainWind.RefreshMoney();
    }

    //tipsWind
    public void Tips(string value)
    {
        tipsWind.SetWindState(true);
        tipsWind.Tips(value);
    }

    public void AddEnergyUI()//增加玩家的能量UI
    {
        //增加玩家的能量
        //TODO
    }
    public void SetGunFireUI()//控制UI的炮开火
    {
        mainWind.SetGunFire();
    }
    public void SetGunRotateUI()//控制UI的炮自行旋转
    {
        mainWind.SetGunRotate();
    }
    public Transform GetFirePointTrans()//获取开火点
    {
        return mainWind.GetFirePointTrans();
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
            curIceSkillTime = maxUseIceSkillTime;
            isUseIceSkill = true;
            FishSceneSys.Instance.SetIceSkillState(true);//设置鱼群为冰冻状态
            FishSceneSys.Instance.IceStateStopMove(maxUseIceSkillTime);//暂停鱼的移动
            SetEfMapBgState(true);
            ;
        }
    }
    private void InitIceSkill()//初始化冰冻技能数据
    {
        isCanUseIceSkill = false;
        isUseIceSkill = false;
        curIceSkillTime = 0f;    //切换场景以后 是否保存cd进度？暂时不保存
        maxUseIceSkillTime = Constant.MaxUseIceSkillTime;
        maxCDIceSkillTime = Constant.MaxCDIceSkillTime;
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
                    SetEfMapBgState(false);//关闭地图背景
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

        maxUseScatteringSkillTime = Constant.MaxUseScatteringSkillTime;
        maxCDScatteringSkillTime = Constant.MaxCDScatteringSkillTime;
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
        if (_mapbgef_FA == null)
        {
            _mapbgef_FA = GameObject.Find("mapbgef").GetComponent<Ef_FadeAway>();
        }
    }
    private void SetEfMapBg(string path)//设置特效背景
    {
        //直接显示/使用特效渐变
        mapbgef.sprite = ResSvc.Instance.LoadSprite(PathDefine.MapBg + dataSvc.pd.FishSceneLv.ToString() + path);
    }
    private void SetEfMapBgState(bool state)
    {
        if (state)
        {
            _mapbgef_FA.PlayBackwards();
        }
        else
        {
            _mapbgef_FA.Play();
        }
    }
    #endregion
}