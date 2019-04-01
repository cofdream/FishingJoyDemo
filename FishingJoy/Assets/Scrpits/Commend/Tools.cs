using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class Tools
{

    public static int GetMaxExpByLv(int lv)
    {
        return lv * 200;
    }

    private static float levy = 0.05000f;
    public static float GetFishingProbability(int gunMoney, int fishMoney)//获取捕鱼概率
    {
        return gunMoney * (1 - levy) / fishMoney;
    }

    public static int GetGunMoney(int gunlv)
    {
        return (gunlv + 1) * 5;
    }
}