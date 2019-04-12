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
                transform.Translate(direction * Time.unscaledDeltaTime * speed);
            }
            else
            {
                pauseTime -= Time.unscaledDeltaTime;
                if (pauseTime <= 0f)
                {
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

    public void SetMoveState(bool state = true)
    {
        isMove = state;
    }
    public void Pause(float time)//暂停移动时间
    {
        if (time > pauseTime) //覆盖之前的暂停时间
        {
            pauseTime = time;
            StopAnimator();
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