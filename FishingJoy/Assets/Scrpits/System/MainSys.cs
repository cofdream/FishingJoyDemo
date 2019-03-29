using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//主游戏场景业务系统
public class MainSys : MonoBehaviour
{
    public static MainSys Instance { get; private set; }

    private MianWind mianWind;
    private bool isFire = false;

    public void InitSys()
    {
        Instance = this;
        mianWind = transform.Find("Canvas/MianWind").GetComponent<MianWind>();

        Debug.Log("Init MainSys Done.");
    }

    private void Update()
    {
        if (isFire)
        {
#if UNITY_ANDROID || UNITY_IPHONE //移动端判断
             if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
#else
            if (EventSystem.current.IsPointerOverGameObject())
#endif
            {
                return;
            }
            if (Input.GetMouseButtonDown(0))
            {
                SetGunRotate();
                SetGunFire();
            }
        }

    }

    public void EnterGame()
    {
        isFire = true;
        OpenMainWind();
    }
    public void QuitGame()
    {
        isFire = false;
        CloseMainWind();
    }

    public void SetGunRotate()//炮的旋转
    {
        mianWind.SetGunRotate();
    }
    public void SetGunFire()//炮的开火
    {
        mianWind.SetGunFire();
    }


    //MainWind
    public void OpenMainWind()
    {
        mianWind.SetWindState();
    }
    public void CloseMainWind()
    {
        mianWind.SetWindState(false);
    }

}