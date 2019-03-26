using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//玩家控制器
public class GameController : MonoBehaviour
{
    public GameController Instance { get; private set; }

    private GameSceneMgr gameSceneMgr;
    private Vector3 point;
    private Vector3 gunPos;
    private float angle;
    private RectTransform canvasRect;

    public void Init()
    {
        Instance = this;
        gameSceneMgr = GameSceneMgr.Instance;
        canvasRect = transform.Find("Canvas").GetComponent<RectTransform>();
        gunPos = gameSceneMgr.GetGunPos().position;

        Debug.Log("Init GameController Done.");
    }

    public void MyUpdate()
    {
        if (gameSceneMgr.enterGame == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                SetGunAngle();
                gameSceneMgr.SetFire(point);
            }
        }
    }
    //设置炮的旋转
    private void SetGunAngle()
    {

        GetWorldPointInRectangle(Input.mousePosition, out point);
        angle = Vector3.Angle(Vector3.up, point - gunPos);
        if (angle > Constant.GunMaxAngle)
        {
            return;
        }
        if (point.x > gunPos.x)
        {
            angle = -angle;
        }
        gameSceneMgr.SetGunAngles(angle);
    }
    //屏幕坐标转换成世界坐标
    private void GetWorldPointInRectangle(Vector2 targetPos, out Vector3 point)
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(canvasRect, targetPos, Camera.main, out point);
    }
}