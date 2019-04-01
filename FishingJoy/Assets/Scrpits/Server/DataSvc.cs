﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//游戏数据管理
public class DataSvc : MonoBehaviour
{
    public static DataSvc Instance { get; private set; }
    public void InitSvc()
    {
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
    }
    private void RemoveAllPlayerData()
    {
        PlayerPrefs.DeleteAll();
    }

    public void AddGold(int gold)
    {
        pd.Gold += gold;
    }
    public void AddDiamond(int diamond)
    {
        pd.Diamond += diamond;
    }
    int maxExp = 0;
    public void AddExp(int exp)
    {
        if (exp <=0) return;

        pd.Exp += exp;

        while (true)
        {
            if (pd.Exp >= maxExp)
            {
                pd.Exp -= maxExp;
                pd.Lv++;
                maxExp = Tools.GetMaxExpByLv(pd.Lv);
            }
            else
            {
                return;
            }
        }
    }
    public void AddGunLv(int gunLv)
    {
        pd.GunLv += gunLv;
    }

}