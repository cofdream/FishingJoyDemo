using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//移动
public class Move : MonoBehaviour
{

    private Vector3 direction;

    private float speed;

    private void Update()
    {
        transform.Translate(direction * Time.deltaTime * speed);

    }

    public void Init(Vector3 direction, float speed)
    {
        SetDirection(direction);
        SetSpeed(speed);
    }
    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
    }
}