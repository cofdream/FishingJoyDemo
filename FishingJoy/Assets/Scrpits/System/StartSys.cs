using UnityEngine;
using UnityEngine.UI;

//开始业务系统
public class StartSys : MonoBehaviour
{
    public static StartSys Instance { get; private set; }

    private StartWind startWind;
    private LoadingWind loadingWind;
    private DataSvc dataSvc;

    private Transform createPaoPaoEfParent;

    public void InistSys()
    {
        Instance = this;
        startWind = transform.Find("Canvas/StartWind").GetComponent<StartWind>();
        loadingWind = transform.Find("Canvas/LoadingWind").GetComponent<LoadingWind>();
        dataSvc = DataSvc.Instance;

        InitPaoPaoEF();//初始化泡泡特效


        Debug.Log("Init StartSys Done.");
    }

    public void EnterStart()//进入开始场景
    {
        OpenStartWind();
        SetStartAudio();//设置场景音效
        StarsTwinkleEf();//开始星星特效
        CreatePaoPaoEF();//生成泡泡特效
    }
    public void ExitStart()//退出开始场景
    {
        CleatPaoPaoEF();//清除泡泡特效
        EndStarsTwinkleEf();
    }

    public void EnterGame()//进入游戏场景
    {
        CloseStartWind();
        MainSys.Instance.EnterGame();
    }

    //StartWind
    public void OpenStartWind()
    {
        startWind.SetWindState();
    }
    public void CloseStartWind()
    {
        startWind.SetWindState(false);
    }

    //LoadingWind
    public void OpenLoadingWind()
    {
        loadingWind.SetWindState();
    }
    public void CloseLoadingWind()
    {
        loadingWind.SetWindState(false);
    }
    public void SetProgress(float value)
    {
        loadingWind.SetProgress(value);
    }

    //音效
    public void SetStartAudio()
    {
        AudioSvc.Instance.PlayBgAudio(PathDefine.BGMusic_Main);
        AudioSvc.Instance.SetBgAudioVolume(dataSvc.pd.BgVolume);
        AudioSvc.Instance.SetUIAudioVolume(dataSvc.pd.UIVolume);
    }

    //生成一些场景特效
    private void InitPaoPaoEF()
    {
        createPaoPaoEfParent = GameObject.Find("CreatePaoPaoEf").transform;
    }
    private void CreatePaoPaoEF()
    {

    }
    private void CleatPaoPaoEF()
    {
        //生成2D泡泡
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
    private void StarsTwinkleEf()
    {
        startWind.StarsTwinkleEf();
    }
    private void EndStarsTwinkleEf()
    {
        startWind.EndTwinkleEf();
    }
}