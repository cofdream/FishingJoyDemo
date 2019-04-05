using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//透明度闪烁效果
public class Ef_Flicker : MonoBehaviour
{

    public bool loop;
    public float speed;

    private float alpha;

    protected bool isAdd = true;
    protected bool isStart = true;


    protected virtual void SetSpeed(float alpha)
    {
        if (isAdd)
        {
            if (alpha >= 1f)
            {
                isAdd = false;
                speed = -speed;
            }
        }
        else
        {
            if (alpha <= 0.1f)
            {
                isAdd = true;
                speed = -speed;
                if (loop == false)
                {
                    isStart = false;
                }
            }
        }
    }
}