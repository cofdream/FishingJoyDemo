using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StartWind : WindBase
{
    private Transform allStar;
    private Button btn_Start;

    protected override void InitWind()
    {
        base.InitWind();

        allStar = GetComp<Transform>("top/star");
        btn_Start = GetComp<Button>("center/btn_Start");
        btn_Start.onClick.AddListener(OnClickStart);

        ShowPaoPao();

        Debug.Log("Init StartWind Done.");
    }

    public void RefreshUI()
    {
        PlayerData pd = DataSvc.Instance.pd;
        //TODO 设置音量的默认值
    }

    //生成一些场景特效
    private void ShowPaoPao()
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

            float x = Random.Range(0, 0.5f);
            float y = Random.Range(0, 0.5f);

            Move move = temp.AddComponent<Move>();
            move.Init(new Vector3(-x, y, 0), Constant.PaoPaoSpeed);
        }
    }

    private void OnClickStart()
    {
        audioSvc.PlayUIAudio(PathDefine.UIClick);
        audioSvc.StopBgAudio();//停止背景音效
        ResSvc.Instance.LoadSceneAsync(PathDefine.GameScene, () =>
        {
            StartSys.Instance.EnterGame();//进入游戏场景 加载游戏相关配置
        });
    }
}