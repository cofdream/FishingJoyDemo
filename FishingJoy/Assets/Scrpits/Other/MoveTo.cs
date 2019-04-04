using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//移动自己到另一个位置，移动结束以后可以回调
public class MoveTo : MonoBehaviour
{

    public Action callBack;
    public Vector3 targetPos;
    public float moveSpeed;
    private bool isMove = false;

    private void Update()
    {
        if (isMove)
        {
            MoveToTarget();
            if (Vector3.Distance(transform.position, targetPos) <= 0.1f)
            {
                if (callBack != null)
                {
                    callBack();
                }
                isMove = false;
            }
        }
    }
    private void MoveToTarget()
    {
        transform.Translate(targetPos * Time.deltaTime * moveSpeed);
    }

    public void SetMove(Vector3 pos, float speed, Action callback = null)
    {
        this.callBack = callback;
        targetPos = pos;
        moveSpeed = speed;
        isMove = true;
    }
}