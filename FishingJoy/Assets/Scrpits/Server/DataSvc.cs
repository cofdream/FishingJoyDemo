using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//游戏数据管理
public class DataSvc : MonoBehaviour
{
    public static DataSvc Instance { get; private set; }

    private int maxExp;
    public void InitSvc()
    {
        RemoveAllPlayerData();
        Instance = this;
        pd = new PlayerData();
        ReadPlayerData();
        maxExp = Tools.GetMaxExpByLv(pd.Lv);

        Debug.Log("Init DataSvc Done.");
    }

    public PlayerData pd { get; private set; }

    private void ReadPlayerData()
    {
        pd.Lv = PlayerPrefs.GetInt(PDType.Lv.ToString(), Constant.PDLv);
        pd.GunLv = PlayerPrefs.GetInt(PDType.GunLv.ToString(), Constant.PDGunLv);
        pd.Gold = PlayerPrefs.GetInt(PDType.Gold.ToString(), Constant.PDGold);
        pd.Diamond = PlayerPrefs.GetInt(PDType.Diamond.ToString(), Constant.PDDiamond);
        pd.FishSceneLv = PlayerPrefs.GetInt(PDType.FishSceneLv.ToString(), Constant.PDFishSceneLv);
        pd.Exp = PlayerPrefs.GetInt(PDType.Exp.ToString(), Constant.PDExp);
        pd.GunEnergy = PlayerPrefs.GetFloat(PDType.GunEnergy.ToString(), Constant.PDGunEnergy);
        pd.BgVolume = PlayerPrefs.GetFloat(PDType.BgVolume.ToString(), Constant.BgVolume);
        pd.UIVolume = PlayerPrefs.GetFloat(PDType.UIVolume.ToString(), Constant.UIVolume);
    }
    private void SavePlayerData()
    {
        PlayerPrefs.SetInt(PDType.Lv.ToString(), pd.Lv);
        PlayerPrefs.SetInt(PDType.GunLv.ToString(), pd.GunLv);
        PlayerPrefs.SetInt(PDType.Gold.ToString(), pd.Gold);
        PlayerPrefs.SetInt(PDType.Diamond.ToString(), pd.Diamond);
        PlayerPrefs.SetInt(PDType.FishSceneLv.ToString(), pd.FishSceneLv);
        PlayerPrefs.SetInt(PDType.Exp.ToString(), pd.Exp);
        PlayerPrefs.SetFloat(PDType.GunEnergy.ToString(), pd.GunEnergy);
        PlayerPrefs.SetFloat(PDType.BgVolume.ToString(), pd.BgVolume);
        PlayerPrefs.SetFloat(PDType.UIVolume.ToString(), pd.UIVolume);
    }
    private void RemoveAllPlayerData()
    {
        PlayerPrefs.DeleteAll();
    }

    public void AddGold(int gold)
    {
        pd.Gold += gold;
        MainSys.Instance.RefreshMoney();
    }
    public void AddDiamond(int diamond)
    {
        pd.Diamond += diamond;
        MainSys.Instance.RefreshMoney();
    }
    public void AddExp(int exp)
    {
        if (exp <= 0) return;

        pd.Exp += exp;

        while (true)
        {
            if (pd.Exp >= maxExp)
            {
                pd.Exp -= maxExp;
                pd.Lv++;
                maxExp = Tools.GetMaxExpByLv(pd.Lv);
                MainSys.Instance.RefreshExpAndLv();//更新UI
                MainSys.Instance.Tips("恭喜你！ 等级升到了" + pd.Lv.ToString() + "级。");
            }
            else
            {
                 MainSys.Instance.RefreshExpAndLv();//更新UI
                return;
            }
        }
    }
    public void AddGunLv(int gunLv)
    {
        pd.GunLv += gunLv;
        if (pd.GunLv < 0)
        {
            pd.GunLv = Constant.PDMaxGunLv;
        }
        else if (pd.GunLv > Constant.PDMaxGunLv)
        {
            pd.GunLv = 0;
        }
        MainSys.Instance.RefreshGunUI();
    }

    public void AddFishSceneLv(int lv)
    {
        pd.FishSceneLv += lv;
        if (pd.FishSceneLv < 1)
        {
            pd.FishSceneLv = 3;
        }
        else if (pd.FishSceneLv > 3)
        {
            pd.FishSceneLv = 1;
        }
    }

    public void AddBgAudioVolume(float volume)
    {
        if (volume > 1f || volume < 0f) return;
        pd.BgVolume = volume;
    }
    public void AddUIAudioVolume(float volume)
    {
        if (volume > 1f || volume < 0f) return;
        pd.UIVolume = volume;
    }


    private void OnDestroy()
    {
        SavePlayerData();
    }
}