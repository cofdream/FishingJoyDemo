using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//移动到目标坐标
public class MoveTargetPos : MonoBehaviour
{

    public Transform target;

    private float speed;
    public bool isStart;

    private void Update()
    {
        MoveFun();
    }

    public void Init(Transform target, float speed)
    {
        SetDirection(target);
        SetSpeed(speed);

        isStart = true;
    }
    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
    public void SetDirection(Transform target)
    {
        this.target = target;
    }

    public void MoveFun()
    {
        if (isStart == false) return;

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            isStart = false;
            ObjectPool.Instance.Put(name, gameObject);
            return;
        }
        transform.position = Vector3.Lerp(transform.position, target.position, 1 / Vector3.Distance(transform.position, target.position) * Time.deltaTime * speed);
    }
}