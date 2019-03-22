﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//
public class UIStart : MonoBehaviour
{

    private Transform allStar;
    private Button btn_Start;

    private void Awake()
    {
        allStar = transform.Find("top/star");
        btn_Start = transform.Find("center/btn_Start").GetComponent<Button>();

        for (int i = 0; i < allStar.childCount; i++)
        {
            allStar.GetChild(i).gameObject.AddComponent<Ef_ImageShine>().Init(Constant.StarShineSpeed, Constant.StarRotateSpeed);
        }
        btn_Start.gameObject.AddComponent<Ef_ImageShine>().Init(Constant.BtnStartShineSpeed, 0, Constant.BtnStartShineMinAlpha, false);

    }

    private void Start()
    {
        AddPaoPao();
    }

    private void Update()
    {

    }

    void AddPaoPao()
    {
        GameObject go = new GameObject("CreatePaoPao");
        RectTransform rect = go.AddComponent<RectTransform>();
        rect.SetParent(this.transform.Find("buttomRight"));
        rect.localPosition = new Vector3(-160, 140, 0);
        rect.localScale = Vector3.one;

        for (int i = 0; i < Constant.PaoPaoCount; i++)
        {
            Sprite sp = ResSvc.Instance.LoadSprite(PathDefine.paopaPath);

            GameObject temp = new GameObject();
            RectTransform rect2 = temp.AddComponent<RectTransform>();
            temp.AddComponent<Image>().sprite = sp;

            rect2.SetParent(go.transform);
            rect2.localPosition = Vector3.zero;
            rect2.localScale = Vector3.one;

            float x = Random.Range(0, 1f);
            float y = Random.Range(0, 1f);

            temp.AddComponent<Move>().Init(new Vector3(-x, y, 0), Constant.PaoPaoSpeed);
        }
    }
}