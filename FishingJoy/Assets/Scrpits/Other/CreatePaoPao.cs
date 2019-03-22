using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//创建物体
public static class CreatePaoPao
{

    public static GameObject CreateSprite(string path)
    {
        Sprite sp = Resources.Load<Sprite>(path);
        GameObject go = new GameObject();
        go.AddComponent<RectTransform>();
        go.AddComponent<Image>().sprite = sp;
        return go;
    }
}