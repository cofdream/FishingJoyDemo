using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//
public class Test : MonoBehaviour {

    public Button btn;
    private void Start () {
        btn.onClick.AddListener(()=> {
            print("OnClick" + btn.name + "时间状态" + Time.timeScale);
        });

    }
	
	private void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Time.timeScale = 0;
            print(Time.timeScale);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Time.timeScale = 1;
            print(Time.timeScale);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Time.timeScale = 2;
            print(Time.timeScale);
        }


    }
}