using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//UI界面基类
public class WindBase : MonoBehaviour
{
    protected ResSvc resSvc;
    protected AudioSvc audioSvc;
    public void SetWindState(bool state = true)
    {
        gameObject.SetActive(state);
        if (state)
        {
            InitWind();
        }
        else
        {
            Clear();
        }
    }

    protected virtual void InitWind()
    {
        resSvc = ResSvc.Instance;
        audioSvc = AudioSvc.Instance;
    }
    protected virtual void Clear()
    {
        resSvc = null;
        audioSvc = null;
    }

    public bool GetWindState()
    {
        return gameObject.activeSelf;
    }

    #region Tool Functions
    protected void SetActive(Transform trans, bool state = true) { trans.gameObject.SetActive(state); }
    protected void SetActive(RectTransform rectTrans, bool state = true) { rectTrans.gameObject.SetActive(state); }
    protected void SetActive(Image img, bool state = true) { img.transform.gameObject.SetActive(state); }
    protected void SetActive(Text txt, bool state = true) { txt.transform.gameObject.SetActive(state); }

    protected void SetText(Text text, string context = "") { text.text = context; }
    protected void SetText(Text text, int context = 0) { text.text = context.ToString(); }
    protected void SetText(Transform trans, string context = "") { SetText(trans.GetComponent<Text>(), context); }

    protected void SetSprite(Image img, string path,bool isCache)
    {
        Sprite sp = resSvc.LoadSprite(path, isCache);
        img.sprite = sp;
    }
    //设置图片  取切割后的index为需要设置的下标
    protected void SetSpriteArray(Image img, string path,int index, bool isCache)
    {
        Sprite[] sp = resSvc.LoadSprites(path, isCache);
        img.sprite = sp[index];
    }

    protected T GetComp<T>(string path, Transform trans = null) where T : Component
    {
        if (trans == null)
        {
            trans = transform;
        }
        return trans.Find(path).GetComponent<T>();
    }

    protected T GetOrAddComp<T>(GameObject go) where T : Component
    {
        T t = go.GetComponent<T>();
        if (t == null)
        {
            t = go.AddComponent<T>();
        }
        return t;
    }
    #endregion
}