/****************************************************
    文件：FishRotate.cs
	作者：cofdream
    邮箱: cofdream@sina.com
    日期：2019-04-11-14:40:04
	功能：鱼自由旋转
*****************************************************/

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FishRotate : MonoBehaviour
{
    public int minZ;
    public int maxZ;
    private Vector3 curRotate;

    public float maxTime;
    private float curTime;
    private float speed;

    private float pauseTime;

    public bool isRotate;

    void Update()
    {
       
        if (isRotate)
        {
            if (pauseTime <= 0)
            {
                curTime += Time.deltaTime;
                if (curTime >= maxTime)
                {
                    curTime = 0;
                    Rotate();
                }
            }
            else
            {
                pauseTime -= Time.deltaTime;
            } 
        }
    }

    public void SetState(bool state = true)
    {
        isRotate = state;
    }
    public void Init(int minZ, int maxZ, float maxTime, float speed, bool state = true)
    {
        this.maxTime = maxTime;
        this.maxZ = maxZ;
        this.minZ = minZ;
        this.speed = speed;
        SetState(state);
        pauseTime = 0;
    }

    public void Pause(float time)//暂停旋转 时间
    {
        if (time > pauseTime) //覆盖之前的暂停时间
        {
            pauseTime = time;
        }
    }

    private void Rotate()
    {
        RamondRotate();//随机一个旋转坐标
        //transform.Rotate(curRotate);
        transform.DORotate(curRotate, speed).SetEase(Ease.Linear).SetRelative(true);
    }
    private void RamondRotate()
    {
        float value = Random.Range(minZ, maxZ);
        value = Random.Range(0, 2) == 0 ? value : -value;
        curRotate = new Vector3(0, 0, value);
    }

}
