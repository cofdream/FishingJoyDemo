/****************************************************
    文件：Money.cs
	作者：cofdream
    邮箱: cofdream@sina.com
    日期：2019-04-10-14:10:35
	功能：资金类
*****************************************************/

using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{

    void Update()
    {

    }

    public void MoveToTarget(Vector3 target, float speed, Action callBack = null)
    {
        transform.DOMove(target, speed).SetEase(Ease.Linear).OnComplete(() =>
        {
            if (callBack != null)
            {
                callBack();
                ObjectPool.Instance.Put(name, gameObject);
            }
        });
    }
}
