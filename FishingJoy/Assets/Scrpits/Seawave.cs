/****************************************************
    文件：Seawave.cs
	作者：cofdream
    邮箱: cofdream@sina.com
    日期：2019-04-10-11:32:47
	功能：海浪的脚本,用于换场的切换显示
*****************************************************/

using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seawave : MonoBehaviour
{
    private float _moveX = -14f;
    private float _speed = 4f;

    public void StartSeawave(Action callBack = null)
    {
        transform.position = Vector3.zero;
        transform.DOMoveX(_moveX, _speed).SetEase(Ease.Linear)
            .OnComplete(() =>
                {
                    if (callBack != null)
                    {
                        callBack();
                    }
                });
    }
}
