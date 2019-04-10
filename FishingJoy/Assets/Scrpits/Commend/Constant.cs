using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//常量
public static class Constant
{
    public const float StarShineSpeed = 1.7f;//星星的透明的变化速度
    public const float StarRotateSpeed = -30f;//星星的旋转角度的变化速度

    public const float BtnStartShineSpeed = 0.7f;//开始按钮的透明的变化速度
    public const float BtnStartShineMinAlpha = 0.6f;//开始按钮的透明的变化下限

    public const int PaoPaoCount = 5;//创建泡泡的数量
    public const float PaoPaoSpeed = 2f;//泡泡的移动速度
    public const float PaoPaoRotate = 60f;//创建泡泡的最大随机角度

    public const float GunMaxAngle = 75f;//炮最大旋转角度


    public const float ChangeFishScene = 15f;//换场的时间

    //SkillCD
    public const float MaxUseIceSkillTime = 3f;
    public const float MaxCDIceSkillTime = 15f;
    public const float MaxUseScatteringSkillTime = 5f;
    public const float MaxCDScatteringSkillTime = 20f;

    //游戏数据默认值
    public const int PDLv = 1;
    public const int PDGunLv = 0;
    public const int PDGold = 1000;
    public const int PDDiamond = 100;
    public const int PDFishSceneLv = 1;
    public const int PDExp = 0;
    public const float PDGunEnergy = 0;
    public const int PDMaxGunLv = 2;
    public const float BgVolume = 1f;
    public const float UIVolume = 1f;
}