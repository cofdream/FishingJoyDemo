using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//减少透明度的闪烁，透明度为0时回收当前物体
public class Ef_Flicker_2D : MonoBehaviour
{
    public SpriteRenderer sp;
    public float speed;

    private float alpha;
    private bool isAdd;

    private void Update()
    {
        if (isAdd)
        {
            Flicker();
        }
    }
    public void SetEf(float speed, SpriteRenderer sp = null)
    {
        this.speed = speed;
        if (sp != null)
        {
            this.sp = sp;
        }
        else
        {
            sp = GetComponentInChildren<SpriteRenderer>();
        }
    }
    public void Init()
    {
        isAdd = true;
        sp.color += new Color(0, 0, 0, 1f);
    }

    private void Flicker()
    {
        sp.color -= new Color(0, 0, 0, Time.deltaTime * speed);
        if (sp.color.a <= 0.1f)
        {
            isAdd = false;
        }
    }

}