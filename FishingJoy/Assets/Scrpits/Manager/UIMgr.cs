using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
public class UIMgr : MonoBehaviour {


    private void Start () {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        transform.GetChild(0).gameObject.SetActive(true);
    }
	
	private void Update () {
		
	}
}