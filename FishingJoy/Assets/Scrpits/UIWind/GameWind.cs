using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//游戏主界面UI
public class GameWind : WindBase
{
    //topLeft
    private Button btn_Achieve;
    private Image img_Exp;
    private Text tx_lvel;
    //topRight
    private Button btn_Shop;
    //center
    private GameObject tip_Achieve;//成就提示
    //buttom
    private Button btn_Gold;
    private Button btn_Diamond;
    private Button btn_GunDown;
    private Button btn_GunUp;
    private Image mask_Gold;
    private Image mask_Diamond;
    private Image mask_GunDown;
    private Image mask_GunUp;
    private Image energy;//能量条
    private Transform gun;
    private Image img_Gun;
    private Transform firePoint;
    //buttomRight
    private Button btn_Set;


    private bool isInitWind = true;

    public override void Init()
    {
        InitWind();

        Debug.Log("Init GameWind Done.");
    }
    private void InitWind()
    {
        if (isInitWind == false) return;
        isInitWind = false;

        btn_Achieve = transform.Find("topLeft/btn_Achieve").GetComponent<Button>();
        btn_Achieve.onClick.AddListener(OnClickAchieve);
        img_Exp = transform.Find("topLeft/exp/fg").GetComponent<Image>();
        tx_lvel = transform.Find("topLeft/level").GetComponent<Text>();

        btn_Shop = transform.Find("topRight/btn_Shop").GetComponent<Button>();
        btn_Shop.onClick.AddListener(OnClickShop);

        btn_Gold = transform.Find("buttom/bgGold/btn_Add").GetComponent<Button>();
        btn_Gold.onClick.AddListener(OnClickGold);
        btn_Diamond = transform.Find("buttom/bgDiamond/btn_Add").GetComponent<Button>();
        btn_Diamond.onClick.AddListener(OnClickDiamond);
        btn_GunDown = transform.Find("buttom/btn_GunDown").GetComponent<Button>();
        btn_GunDown.onClick.AddListener(OnClickGunDown);
        btn_GunUp = transform.Find("buttom/btn_GunUp").GetComponent<Button>();
        btn_GunUp.onClick.AddListener(OnClickGunUp);
        mask_Gold = transform.Find("buttom/bgGold/mask").GetComponent<Image>();
        mask_Diamond = transform.Find("buttom/bgDiamond/mask").GetComponent<Image>();
        mask_GunDown = transform.Find("buttom/btn_GunDown/mask").GetComponent<Image>();
        mask_GunUp = transform.Find("buttom/btn_GunUp/mask").GetComponent<Image>();
        energy = transform.Find("buttom/energy/fg").GetComponent<Image>();
        img_Gun = transform.Find("buttom/gun/icon").GetComponent<Image>();
        gun = img_Gun.transform.parent;
        firePoint = transform.Find("buttom/gun/FirePoint");

        btn_Set = transform.Find("buttomRight/btn_Set").GetComponent<Button>();
        btn_Set.onClick.AddListener(OnClickSet);
    }

    public void RefreshUI()
    {

    }

    public void SetGunAngles(float angles)//设置炮旋转
    {
        gun.transform.rotation = Quaternion.Euler(0, 0, angles);
    }
    public void SetFire(Vector3 pos)
    {
        //生成开火UI特效
        //TODO
    }
    public Transform GetGunTrans()
    {
        return gun.transform;
    }

    #region Btn
    private void OnClickAchieve()
    {
        if (tip_Achieve.activeSelf == false)
        {
            tip_Achieve.SetActive(true);
        }
    }
    private void OnClickShop()
    {
        Debug.Log("OnClickShop");
    }

    private void OnClickGold()
    {
        Debug.Log("OnClickGold");
    }
    private void OnClickDiamond()
    {
        Debug.Log("OnClickDiamond");
    }
    private void OnClickGunDown()
    {
        Debug.Log("OnClickGunDown");
    }
    private void OnClickGunUp()
    {
        Debug.Log("OnClickGunUp");
    }

    private void OnClickSet()
    {
        Debug.Log("OnClickSet");
    }

    #endregion
}