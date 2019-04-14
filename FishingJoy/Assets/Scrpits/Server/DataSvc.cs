using System;
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
        //RemoveAllPlayerData();
        Instance = this;
        Pd = new PlayerData();
        ReadPlayerData();
        maxExp = Tools.GetMaxExpByLv(Pd.Lv);

        Debug.Log("Init DataSvc Done.");
    }

    public PlayerData Pd { get; private set; }
    private int[] multiplesArray = new int[] { 1, 2, 5, 10 };//倍率等级所用的倍率值
    public int[] MultiplesArray { get { return multiplesArray; } private set { multiplesArray = value; } }

    private void ReadPlayerData()
    {
        Pd.Lv = PlayerPrefs.GetInt(PDType.Lv.ToString(), Constant.PDLv);
        Pd.GunLv = PlayerPrefs.GetInt(PDType.GunLv.ToString(), Constant.PDGunLv);
        Pd.Gold = PlayerPrefs.GetInt(PDType.Gold.ToString(), Constant.PDGold);
        Pd.Diamond = PlayerPrefs.GetInt(PDType.Diamond.ToString(), Constant.PDDiamond);
        Pd.FishSceneLv = PlayerPrefs.GetInt(PDType.FishSceneLv.ToString(), Constant.PDFishSceneLv);
        Pd.Exp = PlayerPrefs.GetInt(PDType.Exp.ToString(), Constant.PDExp);
        Pd.Multiples = PlayerPrefs.GetInt(PDType.Multiples.ToString(), Constant.PDMultiples);
        Pd.GunEnergy = PlayerPrefs.GetFloat(PDType.GunEnergy.ToString(), Constant.PDGunEnergy);
        Pd.BgVolume = PlayerPrefs.GetFloat(PDType.BgVolume.ToString(), Constant.BgVolume);
        Pd.UIVolume = PlayerPrefs.GetFloat(PDType.UIVolume.ToString(), Constant.UIVolume);
    }
    private void SavePlayerData()
    {
        PlayerPrefs.SetInt(PDType.Lv.ToString(), Pd.Lv);
        PlayerPrefs.SetInt(PDType.GunLv.ToString(), Pd.GunLv);
        PlayerPrefs.SetInt(PDType.Gold.ToString(), Pd.Gold);
        PlayerPrefs.SetInt(PDType.Diamond.ToString(), Pd.Diamond);
        PlayerPrefs.SetInt(PDType.FishSceneLv.ToString(), Pd.FishSceneLv);
        PlayerPrefs.SetInt(PDType.Exp.ToString(), Pd.Exp);
        PlayerPrefs.SetInt(PDType.Multiples.ToString(), Pd.Multiples);
        PlayerPrefs.SetFloat(PDType.GunEnergy.ToString(), Pd.GunEnergy);
        PlayerPrefs.SetFloat(PDType.BgVolume.ToString(), Pd.BgVolume);
        PlayerPrefs.SetFloat(PDType.UIVolume.ToString(), Pd.UIVolume);
    }
    private void RemoveAllPlayerData()
    {
        PlayerPrefs.DeleteAll();
    }

    public void AddGold(int gold)
    {
        Pd.Gold += gold;
        MainSys.Instance.RefreshMoney();
    }
    public void AddDiamond(int diamond)
    {
        Pd.Diamond += diamond;
        MainSys.Instance.RefreshMoney();
    }
    public void AddExp(int exp)
    {
        if (exp <= 0) return;

        Pd.Exp += exp;

        while (true)
        {
            if (Pd.Exp >= maxExp)
            {
                Pd.Exp -= maxExp;
                Pd.Lv++;
                maxExp = Tools.GetMaxExpByLv(Pd.Lv);
                MainSys.Instance.RefreshExpAndLv();//更新UI
                MainSys.Instance.Tips("恭喜你！ 等级升到了" + Pd.Lv.ToString() + "级。");
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
        Pd.GunLv += gunLv;
        if (Pd.GunLv < 0)
        {
            Pd.GunLv = Constant.PDMaxGunLv;
        }
        else if (Pd.GunLv > Constant.PDMaxGunLv)
        {
            Pd.GunLv = 0;
        }
        MainSys.Instance.RefreshGunUI();
    }
    public void AddMultiples(int value)
    {
        Pd.Multiples += value;
        if (Pd.Multiples <= 0)
        {
            Pd.Multiples = 0;
        }
        else if (Pd.Multiples >= multiplesArray.Length)
        {
            Pd.Multiples = multiplesArray.Length - 1;
        }
        MainSys.Instance.RefreshMultipes();
    }
    public string GetMultiples()
    {
      return  multiplesArray[Pd.Multiples].ToString();
    }

    public void AddFishSceneLv(int lv)
    {
        Pd.FishSceneLv += lv;
        if (Pd.FishSceneLv < 1)
        {
            Pd.FishSceneLv = 3;
        }
        else if (Pd.FishSceneLv > 3)
        {
            Pd.FishSceneLv = 1;
        }
    }

    public void AddBgAudioVolume(float volume)
    {
        if (volume > 1f || volume < 0f) return;
        Pd.BgVolume = volume;
    }
    public void AddUIAudioVolume(float volume)
    {
        if (volume > 1f || volume < 0f) return;
        Pd.UIVolume = volume;
    }


    private void OnDestroy()
    {
        SavePlayerData();
    }

    
}