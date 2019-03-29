using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    //public string ID { get; set; }//用于联机排行榜吧。

    public int Lv { get; set; }

    public int GunLv { get; set; }

    public int Gold { get; set; }

    public int Diamond { get; set; }

    public int FishSceneLv { get; set; }

    public int Exp { get; set; }

    public float GunEnergy { get; set; }
    //TODO 音效/
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
}
