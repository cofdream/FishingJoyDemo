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

    private void Update()
    {
        if (pauseTime <= 0)
        {
            transform.Translate(direction * Time.deltaTime * speed);
        }
        else
        {
            pauseTime -= Time.deltaTime;
            if (pauseTime <= 0f)
            {
                PlayAnimator();
            }
        }
    }

    public void Init(Vector3 direction, float speed)
    {
        this.speed = speed;
        this.direction = direction;
        pauseTime = 0f;
        ani = GetComponentInChildren<Animator>();
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