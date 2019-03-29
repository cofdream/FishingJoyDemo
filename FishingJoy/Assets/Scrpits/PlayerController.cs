﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//玩家控制器
public class PlayerController : MonoBehaviour
{
    public PlayerController Instance { get; private set; }

    private GameSceneMgr gameSceneMgr;
    private Vector3 point;
    private Vector3 gunPos;
    private float angle;

    public void Init()
    {
        Instance = this;
        gameSceneMgr = GameSceneMgr.Instance;
        gunPos = gameSceneMgr.GetGunTrans().position;

        Debug.Log("Init GameController Done.");
    }

    public void MyUpdate()
    {
        if (gameSceneMgr.enterGame == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SetGunRotate();
                gameSceneMgr.SetFire(point);
            }
        }
    }
    //设置炮的旋转
    private void SetGunRotate()
    {
        //GameRoot.Instance.GetWorldPointInRectangle(Input.mousePosition, out point);
        angle = Vector3.Angle(Vector3.up, point - gunPos);
        if (angle > Constant.GunMaxAngle)
        {
            return;
        }
        if (point.x > gunPos.x)
        {
            angle = -angle;
        }
        gameSceneMgr.SetGunRotate(angle);
    }

}