using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//移动
public class Move : MonoBehaviour
{
    [SerializeField]
    private Vector3 direction;
    [SerializeField]
    private float speed;
    [SerializeField]
    private bool isMove = false;
 
    private void Start()
    {

    }

    private void Update()
    {
        if (!isMove) return;

        transform.Translate(direction * Time.deltaTime * speed);
    }

    public void StartMove()
    {
        SetIsMove();
    }
    public void StopMove()
    {
        SetIsMove(false);
    }
    private void SetIsMove(bool isMove = true)
    {
        this.isMove = isMove;
    }

    public void Init(Vector3 direction,float speed)
    {
        this.direction = direction;
        this.speed = speed;
    }
}