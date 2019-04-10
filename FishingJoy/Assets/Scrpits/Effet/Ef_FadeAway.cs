/****************************************************
    文件：NewBehaviourScript.cs
	作者：cofdream
    邮箱: cofdream@sina.com
    日期：2019-04-10-09:50:04
	功能：让物体的渐渐的看不见
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ef_FadeAway : MonoBehaviour
{
    public float _speed;
    public SpriteRenderer _spriteRenderer;
    private Color _curColor;
    private Tween _tween;
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _curColor = _spriteRenderer.color;
    }

    public void Play()
    {
        _tween = _spriteRenderer.DOColor(new Color(_curColor.r, _curColor.r, _curColor.b, 0f), _speed);
    }
    public void PlayBackwards()
    {
        _tween = _spriteRenderer.DOColor(new Color(_curColor.r, _curColor.r, _curColor.b, 1f), _speed);
    }
}
