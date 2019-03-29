using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//星星闪烁
public class Ef_ImageShine : MonoBehaviour
{

    private Image target;
    private float speed;
    private float rotSpeed;
    private float alpha;
    private bool isAdd;
    private bool isRotate;
    private float minAlpha;//最小透明度

    public void Init(float speed, float rotSpeed,float minAlpha = 0.2f, bool isRotate = true)
    {
        target = GetComponent<Image>();
        isAdd = true;
        this.isRotate = isRotate;
        this.speed = speed;
        this.rotSpeed = rotSpeed;
        this.minAlpha = minAlpha;
    }

    void Update()
    {
        ShowShine();
    }

    void ShowShine()
    {
        if (isRotate)
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * rotSpeed);
        }

        alpha = target.color.a;
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
            if (alpha <= minAlpha)
            {
                isAdd = true;
                speed = -speed;
            }
        }
        target.color += new Color(0, 0, 0, Time.deltaTime * speed);
    }
}