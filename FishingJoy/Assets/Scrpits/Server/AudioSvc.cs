using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//音效管理
public class AudioSvc : MonoBehaviour
{

    public static AudioSvc Instance { get; private set; }

    private AudioSource BgAS;
    private AudioSource UIAS;

    public void InitSvc()
    {
        Instance = this;
        BgAS = transform.Find("AudiSvc/BgSource").GetComponent<AudioSource>();
        UIAS = transform.Find("AudiSvc/UISource").GetComponent<AudioSource>();
    }
    //背景音乐
    public void PlayBgAudio(string pathClip, bool loop = true, bool isCache = true)
    {
        AudioClip clip = ResSvc.Instance.LoadClip(PathDefine.AudioPath + pathClip, isCache);
        BgAS.clip = clip;
        BgAS.loop = loop;
        BgAS.Play();
    }
    //UI按钮音效/打开面板音效
    public void PlayUIAudio(string pathClip, bool loop = false, bool isCache = true)
    {
        AudioClip clip = ResSvc.Instance.LoadClip(PathDefine.AudioPath + pathClip, isCache);
        UIAS.clip = clip;
        UIAS.loop = loop;
        UIAS.Play();
    }

    //设置音量大小
    public void SetBgAudioVolume(float volume)
    {
        BgAS.volume = volume;
        DataSvc.Instance.AddBgAudioVolume(volume);
    }
    public void SetUIAudioVolume(float volume)
    {
        UIAS.volume = volume;
        DataSvc.Instance.AddUIAudioVolume(volume);
    }

    public void PauseBgAudio()
    {
        BgAS.Pause();
    }
    public void StopBgAudio()
    {
        BgAS.Stop();
    }
    public void PauseUIAudio()
    {
        UIAS.Pause();
    }
    public void StopUIAudio()
    {
        UIAS.Stop();
    }
}