﻿
using System;
using UnityEngine;
using UnityEngine.EventSystems;

//主游戏场景业务系统
public class MainSys : MonoBehaviour {
    public static MainSys Instance { get; private set; }

    private BuyWind buyWind;
    private MainWind mainWind;
    private TipsWind tipsWind;
    private ObjectPool pool;
    private DataSvc dataSvc;

    private PlayerController playerController;//玩家控制器

    private Transform moneyParent;

    private bool isPlayState = false;

    // 海浪
    private float seneTime;
    private SpriteRenderer mapbg;

    public void InitSys() {
        Instance = this;
        dataSvc = DataSvc.Instance;
        mainWind = transform.Find("Canvas/MianWind").GetComponent<MainWind>();
        tipsWind = transform.Find("Canvas/TipsWind").GetComponent<TipsWind>();
        buyWind = transform.Find("Canvas/BuyWind").GetComponent<BuyWind>();

        Debug.Log("Init MainSys Done.");
    }

    private void Update() {

    }

    public void EnterGame()//进入游戏场景
    {
        isPlayState = true;
        OpenMainWind();
        FishSceneSys.Instance.EnterFishScene();//进入鱼场
        pool = ObjectPool.Instance;
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        playerController.SetPlayerCtlState(true);

        InitMap2D();
        SetMapBg();//设置地图背景
        PlayeBgAudio();//播放背景音乐

    }
    public void ExitGame()//退出游戏场景
    {
        isPlayState = false;
        CloseMainWind();
        FishSceneSys.Instance.QuitFishScene();//退出渔场
        playerController.SetPlayerCtlState(false);
        //清空子弹
        //清除渔网
        //隐藏特效背景
        //清除金币和钻石

        //BUG 金币在退出场景时候还能显示 等待修复...（目前没有什么好的想法）
    }
    public void PauseGame()//暂停游戏
    {
        FishSceneSys.Instance.PauseFishScene();
    }
    public void ContinueGame()//继续游戏 配合暂停使用
    {
        FishSceneSys.Instance.ContinueGame();
    }

    //炮
    private void SetFire() {
        if (isPlayState) {
            ChangFishScene();//换场计时
        }
    }
    public void AddGunLv(int lv)//修改炮的等级
    {
        dataSvc.AddGunLv(lv);
    }
    public Quaternion GetUIGunRotate()//返回UI炮的旋转角度
    {
        return mainWind.GetGunRotate();
    }
    public void CreateNetFish(Vector3 pos, string fishNetName)//创建渔网
    {
        playerController.CreateNetFish(pos, fishNetName);
    }
    public void AddExp(int fishGold) {
        playerController.AddExp(fishGold);
    }

    //海浪换场
    private void InitMap2D() {
        if (mapbg == null) {
            mapbg = GameObject.Find("mapbg").GetComponent<SpriteRenderer>();
        }
    }
    private void ChangFishScene() //换场
    {
        //判断是否可以换场 可以使用定时换场/等级换场
        seneTime += Time.deltaTime;
        if (seneTime >= Constant.ChangeFishScene) {
            seneTime = 0;
            isPlayState = false;//关闭开火
            //开启换场特效  //清空场景鱼群/调用鱼群的逃跑方法
            GameObject seaWave = pool.Get(PathDefine.SeaWave);
            seaWave.name = PathDefine.SeaWave;
            seaWave.transform.position = Vector3.zero;
            seaWave.GetComponent<Seawave>().StartSeawave(() => {
                isPlayState = true;//开启开火
                pool.Put(seaWave.name, seaWave);
                //重新生成鱼群
                FishSceneSys.Instance.SetAllCreateFishListState(true);
                //切换场景的背景
                dataSvc.AddFishSceneLv(1);
                SetMapBg();
                //切换场景背景音乐
                PlayeBgAudio();
                pool.Put(PathDefine.SeaWave, seaWave);
            });
            //关闭鱼的生成
            FishSceneSys.Instance.SetAllCreateFishListState(false);
        }
    }
    private void SetMapBg()//设置地图背景
    {
        mapbg.sprite = ResSvc.Instance.LoadSprite(PathDefine.MapBg + dataSvc.Pd.FishSceneLv.ToString());
    }

    //MainWind
    public void OpenMainWind() {
        mainWind.SetWindState();
    }
    public void CloseMainWind() {
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
        mainWind.RefreshGunUI();
    }
    public void RefreshMultipes()//刷新炮的等级
    {
        mainWind.RefreshMultipes();
    }

    public void SetIceSkillCD(float value)//Ice
    {
        mainWind.SetIceSkillCD(value);
    }
    public void SetIceSkillMask(bool state) {
        mainWind.SetIceSkillMask(state);
    }

    public void SetScatteringSkillCD(float value)//Scattering
    {
        mainWind.SetScatteringSkillCD(value);

    }
    public void SetScatteringSkillMask(bool state) {
        mainWind.SetScatteringSkillMask(state);
    }


    //tipsWind
    public void Tips(string value) {
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

    //BuyWind
    public void OpenBuyWind() {
        buyWind.SetWindState(true);
    }
    public void CloseBuyWind() {
        buyWind.SetWindState(false);
    }

    //资金
    public void CreateGoldAndDimand(Transform pos, int gold, int diamond) {
        GameObject go;
        if (gold > 0) {
            go = pool.Get(PathDefine.Gold);
            go.transform.SetParent(moneyParent);
            go.transform.position = pos.position;
            go.name = PathDefine.Gold;

            Money mo = go.GetComponent<Money>();
            mo.MoveToTarget(mainWind.GetGoldPos().position, 1.5f, () => {
                dataSvc.AddGold(gold);
            });

            AudioSvc.Instance.PlayUIAudio(PathDefine.EfGetGold, false, true);
        }
        if (diamond > 0) {
            go = pool.Get(PathDefine.Diamond);
            go.transform.SetParent(moneyParent);
            go.transform.position = pos.position;
            go.name = PathDefine.Diamond;
            Money mo = go.GetComponent<Money>();
            mo.MoveToTarget(mainWind.GetDiamondPos().position, 1.5f, () => {
                dataSvc.AddDiamond(diamond);
            });

            AudioSvc.Instance.PlayUIAudio(PathDefine.EfGetDiamond, false, true);
        }
    }
    public void NotMoneyAnime() {
        mainWind.NotMoneyAnime();
    }
    public void NotDiamondAnime() {
        mainWind.NotDiamondAnime();
    }
    public void AddGoldTipsAnimator(int value)//增加金币的提示动画
    {
        mainWind.AddGoldTipsAnimator(value);
    }
    public void AddDiamondTipsAnimator(int value)//增加钻石的提示动画
    {
        mainWind.AddDiamondTipsAnimator(value);
    }
    public void BuyGold(int gold) {
        dataSvc.AddGold(gold);
        Debug.Log("购买了" + gold.ToString() + "个金币");
    }
    public void BuyDiamond(int diamond) {
        dataSvc.AddDiamond(diamond);
        Debug.Log("购买了" + diamond.ToString() + "个钻石");
    }


    //背景音乐
    private void PlayeBgAudio() {
        string path;
        switch (dataSvc.Pd.FishSceneLv) {
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
    public void OnClickIce() {
        playerController.StartSkillIce();
    }
    public void OnClickFire() {
        //打开燃烧背景
        //TODO
    }
    public void OnClickScattering() {
        playerController.StartSkillScattering();
    }
    #endregion
}