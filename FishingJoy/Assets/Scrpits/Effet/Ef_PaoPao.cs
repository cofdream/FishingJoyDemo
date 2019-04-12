/****************************************************
    文件：Ef_PaoPao.cs
	作者：cofdream
    邮箱: cofdream@sina.com
    日期：2019-04-09-23:09:13
	功能：#Function#
*****************************************************/

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ef_PaoPao : MonoBehaviour
{
    private List<Transform> PaoPaoArray;
    void Start()
    {
        Init();
        Pause();
        Play();
    }
    private void Init()
    {
        PaoPaoArray = new List<Transform>();
        for (int i = 0; i < transform.childCount; i++)
        {
            PaoPaoArray.Add(transform.GetChild(i));  
            PaoPaoArray[i].DOMoveY(4f, 5f)
                .SetRelative(true).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear).SetDelay(i * 1.2f);
            PaoPaoArray[i].DOScale(new Vector3(0.8f, 0.8f, 0.8f), 5f)
                .SetRelative(false).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear).SetDelay(i * 1.2f);
        }
    }
    public void Play()
    {
        for (int i = 0; i < PaoPaoArray.Count; i++)
        {
            PaoPaoArray[i].DOPlay();
        }
    }
    public void Pause()
    {
        for (int i = 0; i < PaoPaoArray.Count; i++)
        {
            PaoPaoArray[i].DOPause();
        }
    }
}
