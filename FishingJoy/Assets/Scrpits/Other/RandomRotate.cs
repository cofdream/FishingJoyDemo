using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//随机改变旋转方向
public class RandomRotate : MonoBehaviour
{

    private int curRotCount;//当前旋转次数
    private int maxRotCount = 3;//最大旋转次数
    private int maxRotAngle = 360;
    private float time;
    private float maxTime = 4f;

    private float targetRot; //目标旋转角度
    private void Update()
    {
        if (curRotCount >= maxRotCount)
        {
            return;
        }
        time += Time.deltaTime;
        if (time >= maxTime)
        {
            targetRot = Random.Range(0, maxRotAngle);
            isLerpRot = true;
            time = 0;
            curRotCount++;
        }

        if (isLerpRot)
        {
            SmoothnessRotate();
        }
    }

    Quaternion taQua;
    bool isLerpRot = false;
    private void SmoothnessRotate()
    {
        taQua = Quaternion.Euler(0, 0, targetRot);

        transform.rotation = Quaternion.Lerp(transform.rotation, taQua, 1.5f);
    }

}