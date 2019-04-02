using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//音效管理
public class AudioSvc : MonoBehaviour
{

    public AudioSvc Instance { get; private set; }

    private AudioSource BgAS;
    private AudioSource UIAS;
    private AudioSource AS;

    public void InitSvc()
    {
        Instance = this;
        BgAS = transform.Find("BgAudioSource").GetComponent<AudioSource>();
        UIAS = transform.Find("UIAudioSource").GetComponent<AudioSource>();
        AS = transform.Find("SceneAudioSource").GetComponent<AudioSource>();
    }
    //背景音乐
    public void PlayerBgAudio(string pathClip, bool loop = true)
    {

    }
    //UI按钮音效/打开面板音效
    public void PlayerUIAudio(string pathClip, bool loop = false)
    {

    }



}