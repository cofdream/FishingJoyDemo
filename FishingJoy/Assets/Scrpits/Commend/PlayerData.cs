using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    //public string ID { get; set; }//TODO用于联机排行榜吧。

    public int Lv { get; set; }

    public int GunLv { get; set; }

    public int Gold { get; set; }

    public int Diamond { get; set; }

    public int FishSceneLv { get; set; }

    public int Exp { get; set; }

    public int Multiples { get; set; }

    public float GunEnergy { get; set; }

    public float BgVolume { get; set; }

    public float UIVolume { get; set; }
}
public enum PDType
{
    Lv,
    GunLv,
    Gold,
    Diamond,
    FishSceneLv,
    GunEnergy,
    Exp,
    Multiples,
    BgVolume,
    UIVolume,
}
