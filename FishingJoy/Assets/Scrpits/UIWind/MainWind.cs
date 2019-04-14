using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//游戏主界面UI
public class MainWind : WindBase
{
    #region UI
    private bool isInitUI = true;
    //topLeft
    private Button btn_Achieve;
    private Image img_Exp;
    private Text tx_lvel;
    //topRight
    private Button btn_Shop;
    //center
    private GameObject tip_Achieve;//成就提示
    private GameObject settingPanle;//设置面板
    private Button btn_CloseSetting;
    private Button btn_BackStartWind;
    private Slider sld_music;
    private Slider sld_EfMusic;

    //buttom
    private Button btn_Gold;
    private Button btn_Diamond;
    private Button btn_GunDown;
    private Button btn_GunUp;
    private Image mask_Gold;
    private Image mask_Diamond;
    private Image mask_GunDown;
    private Image mask_GunUp;
    private Button btn_Multiples;
    private Text tx_Multiples;

    private Transform goldTrans;
    private Transform diamondTrans;

    private Image energy;//能量条
    private Image gun2DIcon; //gun2D
    private Transform firePoint2D;
    private Transform gun2DTrans;

    private Text tx_Gold;
    private Text tx_Diamond;
    //buttomRight
    private Button btn_Set;

    #region Skill
    private Button btn_Ice;
    private Button btn_Fire;
    private Button btn_Scattering;
    private Image ef_Ice;
    private Image ef_Fire;
    private Image ef_Scattering;
    private Image cd_Ice;
    private Image cd_Fire;
    private Image cd_Scattering;
    #endregion

    #endregion

    private DataSvc dataSvc;
    private PlayerData pd;

    private bool isPlayGoldAnime;//是否在播金币不足的动画
    private bool isPlayDiamondAnime;//是否在播金币不足的动画

    protected override void InitWind()
    {
        base.InitWind();
        dataSvc = DataSvc.Instance;
        pd = dataSvc.pd;
        InitUI();
        RefreshUI();

        isPlayGoldAnime = false;
        isPlayDiamondAnime = false;

        Debug.Log("Init MainWind Done.");
    }
    protected override void Clear()
    {
        base.Clear();
        dataSvc = null;
        pd = null;
    }
    private void InitUI()
    {
        if (isInitUI == false) return;
        isInitUI = false;

        btn_Achieve = GetComp<Button>("topLeft/btn_Achieve");
        btn_Achieve.onClick.AddListener(OnClickAchieve);
        img_Exp = GetComp<Image>("topLeft/exp/fg");
        tx_lvel = GetComp<Text>("topLeft/level");

        btn_Shop = GetComp<Button>("topRight/btn_Shop");
        btn_Shop.onClick.AddListener(OnClickShop);

        tip_Achieve = GetComp<Transform>("center/tip_Achieve").gameObject;
        settingPanle = GetComp<Transform>("setting").gameObject;
        btn_CloseSetting = GetComp<Button>("btn_Close", settingPanle.transform);
        btn_CloseSetting.onClick.AddListener(OnClickCloseSettingPanle);
        btn_BackStartWind = GetComp<Button>("btn_Back", settingPanle.transform);
        btn_BackStartWind.onClick.AddListener(OnClickBackStartWind);
        sld_music = GetComp<Slider>("slider_Music", settingPanle.transform);
        sld_music.onValueChanged.AddListener(OnChangeSetMusicSize);
        sld_EfMusic = GetComp<Slider>("slider_EfMusic", settingPanle.transform);
        sld_EfMusic.onValueChanged.AddListener(OnChangeSetEfMusicSize);

        btn_Gold = GetComp<Button>("buttom/bgGold/btn_Add");
        btn_Gold.onClick.AddListener(OnClickGold);
        btn_Diamond = GetComp<Button>("buttom/bgDiamond/btn_Add");
        btn_Diamond.onClick.AddListener(OnClickDiamond);
        btn_GunDown = GetComp<Button>("buttom/btn_GunDown");
        btn_GunDown.onClick.AddListener(OnClickGunDown);
        btn_GunUp = GetComp<Button>("buttom/btn_GunUp");
        btn_GunUp.onClick.AddListener(OnClickGunUp);
        mask_Gold = GetComp<Image>("buttom/bgGold/mask");
        mask_Diamond = GetComp<Image>("buttom/bgDiamond/mask");
        mask_GunDown = GetComp<Image>("buttom/btn_GunDown/mask");
        mask_GunUp = GetComp<Image>("buttom/btn_GunUp/mask");
        energy = GetComp<Image>("buttom/energy/fg");
        tx_Gold = GetComp<Text>("buttom/bgGold/value");
        tx_Diamond = GetComp<Text>("buttom/bgDiamond/value");

        btn_Multiples = GetComp<Button>("buttom/btn_Multiples");
        btn_Multiples.onClick.AddListener(OnClickMultiples);
        tx_Multiples = GetComp<Text>("buttom/btn_Multiples/tx_Multiple");

        goldTrans = GetComp<Transform>("buttom/bgGold/icon");
        diamondTrans = GetComp<Transform>("buttom/bgDiamond/icon");

        //2D枪
        gun2DIcon = GetComp<Image>("buttom/gun/icon");
        gun2DTrans = gun2DIcon.transform.parent;
        firePoint2D = GetComp<Transform>("buttom/gun/firePoint");

        btn_Set = GetComp<Button>("buttomRight/btn_Set");
        btn_Set.onClick.AddListener(OnClickSet);

        btn_Ice = GetComp<Button>("right/btn_Ice");
        btn_Ice.onClick.AddListener(OnClickIce);
        btn_Fire = GetComp<Button>("right/btn_Fire");
        btn_Fire.onClick.AddListener(OnClickFire);
        btn_Scattering = GetComp<Button>("right/btn_Scattering");
        btn_Scattering.onClick.AddListener(OnClickScattering);
        ef_Ice = GetComp<Image>("ef_Ice", btn_Ice.transform);
        ef_Fire = GetComp<Image>("ef_Fire", btn_Fire.transform);
        ef_Scattering = GetComp<Image>("ef_Scattering", btn_Scattering.transform);
        cd_Ice = GetComp<Image>("cd_Ice", btn_Ice.transform);
        cd_Fire = GetComp<Image>("cd_Fire", btn_Fire.transform);
        cd_Scattering = GetComp<Image>("cd_Scattering", btn_Scattering.transform);
    }
    public void RefreshUI()
    {
        RefreshExpAndLv();
        RefreshMoney();
        RefreshGunUI();
    }
    public void RefreshExpAndLv()//刷新等级和经验
    {
        tx_lvel.text = pd.Lv.ToString();
        img_Exp.fillAmount = (float)pd.Exp / Tools.GetMaxExpByLv(pd.Lv);
    }
    public void RefreshMoney()//刷新钱的显示
    {
        tx_Gold.text = pd.Gold.ToString();
        tx_Diamond.text = pd.Diamond.ToString();
    }
    public void RefreshGunUI()//刷新炮的UI图片
    {
        SetSpriteArray(gun2DIcon, PathDefine.GunPath, pd.GunLv, true);
    }

    //炮
    public float CalculateRotationAngle()//计算旋转角度
    {
        Vector3 worldPoint = Vector3.up;
        RectTransform rect = transform.parent.GetComponent<RectTransform>();
        Vector3 gunPoint = firePoint2D.position;

        RectTransformUtility.ScreenPointToWorldPointInRectangle(rect, Input.mousePosition, Camera.main, out worldPoint);
        float angle = Vector3.Angle(Vector3.up, worldPoint - gunPoint);

        if (angle > 75)//判断角度是否超过75
        {
            angle = 75;
        }
        if (worldPoint.x > gunPoint.x)  //炮口朝右边 为负值
        {
            angle = -angle;
        }
        return angle;
    }
    public void SetGunRotate()//设置炮的旋转角度
    {
        gun2DTrans.rotation = Quaternion.Euler(0, 0, CalculateRotationAngle()); //设置旋转角度
    }
    public Quaternion GetGunRotate()//获得炮的旋转角度
    {
        return gun2DTrans.rotation;
    }
    public void SetGunFire()
    {
        //NODO炮开火特效 产生让炮上下抖动效果
    }

    public Transform GetFirePointTrans()//获取开火点
    {
        return firePoint2D;
    }

    //获取金币和钻石的UI坐标
    public Transform GetGoldPos()
    {
        return goldTrans;
    }
    public Transform GetDiamondPos()
    {
        return diamondTrans;
    }
    public void NotMoneyAnime()//没钱的动画
    {
        if (isPlayGoldAnime == false)//防止多次播放导致初始颜色不对
        {
            isPlayGoldAnime = true;
            tx_Gold.DOColor(Color.red, 0.2f).SetLoops(6, LoopType.Yoyo).SetEase(Ease.Linear).OnComplete(() =>
            {
                isPlayGoldAnime = false;
            });
        }
    }
    public void NotDiamondAnime()
    {
        if (isPlayDiamondAnime == false)
        {
            isPlayDiamondAnime = true;
            tx_Diamond.DOColor(Color.red, 0.2f).SetLoops(6, LoopType.Yoyo).SetEase(Ease.Linear).OnComplete(() =>
            {
                isPlayDiamondAnime = false;
            });
        }
    }

    //设置面板
    private void SetSettingPanleState(bool state = true)//设置设置面板的显示状态
    {
        settingPanle.gameObject.SetActive(state);
        GameRoot.Instance.SetTimeState(state);//暂停时间
    }

    //技能
    //散射
    public void SetScatteringSkillCD(float value)//设置散射技能的显示cd进度
    {
        ef_Scattering.fillAmount = value;
    }
    public void SetScatteringSkillMask(bool state)//设置散射技能的使用状态
    {
        cd_Scattering.gameObject.SetActive(state);
    }
    //冰冻
    public void SetIceSkillCD(float value)
    {
        ef_Ice.fillAmount = value;
    }
    public void SetIceSkillMask(bool state)
    {
        cd_Ice.gameObject.SetActive(state);
    }

    #region Btn
    private void OnClickAchieve()//成就
    {
        audioSvc.PlayUIAudio(PathDefine.UIClick);
        //后面可以改成动画
        tip_Achieve.SetActive(true);
        Invoke("CloseTip", 1f);
    }
    void CloseTip()
    {
        tip_Achieve.SetActive(false);
    }
    private void OnClickShop()
    {
        audioSvc.PlayUIAudio(PathDefine.UIClick);
        MainSys.Instance.OpenBuyWind();
        MainSys.Instance.PauseGame();
    }

    private void OnClickGold()
    {
        audioSvc.PlayUIAudio(PathDefine.UIClick);
        MainSys.Instance.OpenBuyWind();
        MainSys.Instance.PauseGame();
    }
    private void OnClickDiamond()
    {
        audioSvc.PlayUIAudio(PathDefine.UIClick);
        MainSys.Instance.OpenBuyWind();
        MainSys.Instance.PauseGame();
    }
    private void OnClickGunDown()
    {
        audioSvc.PlayUIAudio(PathDefine.UIClick);
        MainSys.Instance.AddGunLv(-1);
    }
    private void OnClickGunUp()
    {
        audioSvc.PlayUIAudio(PathDefine.UIClick);
        MainSys.Instance.AddGunLv(1);
    }
    private void OnClickMultiples()
    {
        //TODO设置倍率按钮
    }

    private void OnClickSet()
    {
        SetSettingPanleState();
        MainSys.Instance.PauseGame();
        //刷新滑动条的显示
        sld_music.value = pd.BgVolume;
        sld_EfMusic.value = pd.UIVolume;
    }
    private void OnClickCloseSettingPanle()
    {
        audioSvc.PlayUIAudio(PathDefine.UIClick);
        SetSettingPanleState(false);
        MainSys.Instance.ContinueGame();
    }
    private void OnClickBackStartWind()
    {
        SetSettingPanleState(false);
        MainSys.Instance.ContinueGame();
        MainSys.Instance.ExitGame();
        StartSys.Instance.EnterStart();
    }
    private void OnChangeSetMusicSize(float value)
    {
        audioSvc.SetBgAudioVolume(value);
    }
    private void OnChangeSetEfMusicSize(float value)
    {
        audioSvc.SetUIAudioVolume(value);
    }

    //skillButton
    private void OnClickIce()
    {
        MainSys.Instance.OnClickIce();
        audioSvc.PlayUIAudio(PathDefine.UIClick);
    }
    private void OnClickFire()
    {
        MainSys.Instance.OnClickFire();
        audioSvc.PlayUIAudio(PathDefine.UIClick);
    }
    private void OnClickScattering()
    {
        MainSys.Instance.OnClickScattering();
        audioSvc.PlayUIAudio(PathDefine.UIClick);
    }
    #endregion
}