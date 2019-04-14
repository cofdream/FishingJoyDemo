using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//移动
public class Move : MonoBehaviour
{

    private Vector3 direction;
    private Animator ani;
    private float speed;
    private float pauseTime;
    private bool isMove;

    private void Update()
    {
        if (isMove)
        {
            if (pauseTime <= 0)
            {
                MoveFunc();
            }
            else
            {
                pauseTime -= Time.deltaTime;
                if (pauseTime <= 0)
                {
                    pauseTime = 0;
                    PlayAnimator();
                }
            }
        }
    }

    public void Init(Vector3 direction, float speed)
    {
        this.speed = speed;
        this.direction = direction;
        pauseTime = 0f;
        ani = GetComponentInChildren<Animator>();
        SetMoveState();
    }

    private void MoveFunc()
    {
        transform.Translate(direction * Time.deltaTime * speed);
    }
    public void SetMoveState(bool state = true)
    {
        isMove = state;
        if (ani != null) //可能是子弹状态
        {
            if (state)
            {
                PlayAnimator();
            }
            else
            {
                StopAnimator();
            }
        }
    }
    public void Pause(float time)//暂停移动时间
    {
        if (time > pauseTime) //覆盖之前的暂停时间
        {
            StopAnimator();
            pauseTime = time;
        }
    }

    private void PlayAnimator()
    {
        ani.speed = 1;
    }
    private void StopAnimator()
    {
        ani.speed = 0;
    }
}