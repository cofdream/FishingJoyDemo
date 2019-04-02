using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//游戏主界面UI
public class MainWind : WindBase
{
    #region UI
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

    //buttom
    private Button btn_Gold;
    private Button btn_Diamond;
    private Button btn_GunDown;
    private Button btn_GunUp;
    private Image mask_Gold;
    private Image mask_Diamond;
    private Image mask_GunDown;
    private Image mask_GunUp;

    private Transform goldTrans;
    private Transform diamondTrans;

    private Image energy;//能量条
    private Image gun2DIcon; //gun2D
    private Transform firePoint2D;
    [HideInInspector] public Transform gun2DTrans;

    private Text tx_Gold;
    private Text tx_Diamond;
    //buttomRight
    private Button btn_Set;
    #endregion

    private DataSvc dataSvc;

    protected override void InitWind()
    {
        base.InitWind();
        InitUI();
        RefreshUI();
        dataSvc = DataSvc.Instance;

        Debug.Log("Init MainWind Done.");
    }
    protected override void Clear()
    {
        base.Clear();
        dataSvc = null;
    }

    bool isInitUI = true;
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
        settingPanle = GetComp<Transform>("center/setting").gameObject;
        btn_CloseSetting = GetComp<Button>("btn_Close", settingPanle.transform);
        btn_CloseSetting.onClick.AddListener(OnClickCloseSettingPanle);
        btn_BackStartWind = GetComp<Button>("btn_Back", settingPanle.transform);
        btn_BackStartWind.onClick.AddListener(OnClickBackStartWind);
        sld_music = GetComp<Slider>("slider_Music", settingPanle.transform);
        sld_music.onValueChanged.AddListener(OnChangeSetMusicSize);

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

        goldTrans = GetComp<Transform>("buttom/bgGold/icon");
        diamondTrans = GetComp<Transform>("buttom/bgDiamond/icon");

        //2D枪
        gun2DIcon = GetComp<Image>("buttom/gun/icon");
        gun2DTrans = gun2DIcon.transform.parent;
        firePoint2D = GetComp<Transform>("buttom/gun/firePoint");

        btn_Set = GetComp<Button>("buttomRight/btn_Set");
        btn_Set.onClick.AddListener(OnClickSet);
    }


    public void RefreshUI()
    {
        PlayerData pd = DataSvc.Instance.pd;
        //SetTopLeft
        tx_lvel.text = pd.Lv.ToString();
        img_Exp.fillAmount = (float)pd.Exp / Tools.GetMaxExpByLv(pd.Lv);
        //SetButtom
        tx_Gold.text = pd.Gold.ToString();
        tx_Diamond.text = pd.Diamond.ToString();
        SetSpriteArray(gun2DIcon, PathDefine.GunPath, pd.GunLv, true);
        //SetButtomRight
        //TODO设置音效的大小
    }

    private Vector3 worldPoint;
    private float angle;
    private void CalculateRotationAngle()//计算旋转角度
    {
        RectTransform rect = transform.parent.GetComponent<RectTransform>();
        Vector3 gunPoint = firePoint2D.position;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rect, Input.mousePosition, Camera.main, out worldPoint);
        angle = Vector3.Angle(Vector3.up, worldPoint - gunPoint);
        if (worldPoint.x > gunPoint.x) //炮口朝右边 为负值
        {
            angle = -angle;
        }
    }
    public void SetGunRotate()
    {
        CalculateRotationAngle();
        //设置旋转角度
        gun2DTrans.rotation = Quaternion.Euler(0, 0, angle);
    }
    public void SetGunFire()
    {

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

    #region Btn
    private void OnClickAchieve()//成就
    {
        tip_Achieve.SetActive(true);//后面改为动画
        Invoke("CloseTip", 2f);
    }
    void CloseTip()
    {
        tip_Achieve.SetActive(false);
    }
    private void OnClickShop()
    {
        Debug.Log("OnClickShop");
    }

    private void OnClickGold()
    {
        dataSvc.AddGold(1000);
        RefreshUI();
    }
    private void OnClickDiamond()
    {
        dataSvc.AddDiamond(100);
        RefreshUI();
    }
    private void OnClickGunDown()
    {
        dataSvc.AddGunLv(-1);
        RefreshUI();
    }
    private void OnClickGunUp()
    {
        dataSvc.AddGunLv(1);
        RefreshUI();
    }

    private void OnClickSet()
    {
        settingPanle.gameObject.SetActive(true);
    }
    private void OnClickCloseSettingPanle()
    {
        settingPanle.gameObject.SetActive(false);
    }
    private void OnClickBackStartWind()
    {
        MainSys.Instance.QuitGame();
        StartSys.Instance.EnterStart();
    }
    private void OnChangeSetMusicSize(float value)
    {
        Debug.Log(value.ToString());

    }

    #endregion
}