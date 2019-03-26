using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//UI界面基类
public class WindBase : MonoBehaviour
{

    public void SetWindState(bool state = true)
    {
        gameObject.SetActive(state);
        if (state)
        {
            Create();
        }
        else
        {
            Clear();
        }
    }
    public virtual void Init()
    {

    }

    protected virtual void Create()
    {

    }
    protected virtual void Clear()
    {

    }
}