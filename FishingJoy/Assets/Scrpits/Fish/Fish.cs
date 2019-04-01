using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//鱼
public class Fish : FishBase
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FishWall")
        {
            Put();
        }
    }
}