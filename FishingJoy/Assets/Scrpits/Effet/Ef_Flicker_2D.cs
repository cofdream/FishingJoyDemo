using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
public class Ef_Flicker_2D : Ef_Flicker
{
    public SpriteRenderer sp;

    private void Update()
    {
        Flicker();
    }

    private void Flicker()
    {
        SetSpeed(sp.color.a);
        sp.color += new Color(0, 0, 0, Time.deltaTime * speed);
        Debug.Log(speed);
    }
}