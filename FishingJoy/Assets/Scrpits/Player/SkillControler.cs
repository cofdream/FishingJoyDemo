/****************************************************
    文件：SkillControler.cs
	作者：cofdream
    邮箱: cofdream@sina.com
    日期：2019-04-10-17:23:04
	功能：技能控制器
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillControler : MonoBehaviour
{
    //冰冻
    private bool isCanUseIceSkill;
    public bool IsUseIceSkill { get; private set; }
    private float curIceSkillTime;
    private float maxUseIceSkillTime;
    private float maxCDIceSkillTime;
    //散射
    private bool isCanUseScatteringSkill;//是否可以使用技能
    public bool IsUseScatteringSkill { get; private set; }
    private float curScatteringTime;
    private float maxUseScatteringSkillTime;
    private float maxCDScatteringSkillTime;


    private SpriteRenderer mapbgef;
    private Ef_FadeAway _mapbgef_FA;

    private MainSys mainSys;
    private DataSvc dataSvc;

    private void Start()
    {
        Init();
    }
    private void Init()
    {
        mainSys = MainSys.Instance;
        dataSvc = DataSvc.Instance;
        InitEfMapBg();
        InitIceSkill();
        InitScatteringSkill();
    }

    public void UpdateSkill()
    {
        SkillIce_Timer();
        Skill_Scattering_Timer();
    }

    public void StartSkillIce()//开始冰冻技能
    {
        //打开冰冻背景
        if (isCanUseIceSkill && IsUseIceSkill == false)
        {
            SetEfMapBg("_1");//设置冰冻背景
            curIceSkillTime = maxUseIceSkillTime;
            IsUseIceSkill = true;
            FishSceneSys fishScene = FishSceneSys.Instance;
            fishScene.SetFishListBehaviourState(false);//设置鱼群为冰冻状态
            fishScene.IceStateStopMove(maxUseIceSkillTime);//暂停鱼的移动 鱼群会自动恢复移动
            SetEfMapBgState(true);
        }
    }
    private void InitIceSkill()//初始化冰冻技能数据
    {
        isCanUseIceSkill = false;
        IsUseIceSkill = false;
        curIceSkillTime = 0f;    //切换场景以后 是否保存cd进度？暂时保存
        maxUseIceSkillTime = Constant.MaxUseIceSkillTime;
        maxCDIceSkillTime = Constant.MaxCDIceSkillTime;
    }
    private void SkillIce_Timer()//技能计时器
    {
        //增加计时功能 
        if (isCanUseIceSkill)
        {
            if (IsUseIceSkill)
            {
                curIceSkillTime -= Time.deltaTime;//倒计时
                if (curIceSkillTime <= 0f)
                {
                    //技能使用时间结束
                    curIceSkillTime = 0;
                    //设置不能使用技能 
                    isCanUseIceSkill = false;
                    //使用技能结束
                    IsUseIceSkill = false;
                    SetEfMapBgState(false);//关闭地图背景
                }
                mainSys.SetIceSkillCD(curIceSkillTime / maxUseIceSkillTime);
            }
        }
        else
        {
            curIceSkillTime += Time.deltaTime;
            if (curIceSkillTime >= maxCDIceSkillTime)
            {
                //可以使用技能了
                isCanUseIceSkill = true;
                mainSys.SetIceSkillMask(false);//关闭黑色遮罩
            }
            else
            {
                mainSys.SetIceSkillMask(true);//打开黑色遮罩
            }
            mainSys.SetIceSkillCD(curIceSkillTime / maxCDIceSkillTime);
        }
    }

    public void StartSkillScattering()//开始散射技能
    {
        if (isCanUseScatteringSkill && IsUseScatteringSkill == false)
        {
            curScatteringTime = maxUseScatteringSkillTime;
            IsUseScatteringSkill = true;
        }
    }
    private void InitScatteringSkill()//初始化散射技能数据
    {
        isCanUseScatteringSkill = false;
        IsUseScatteringSkill = false;
        curScatteringTime = 0f;//是否保存cd进度？暂时保存

        maxUseScatteringSkillTime = Constant.MaxUseScatteringSkillTime;
        maxCDScatteringSkillTime = Constant.MaxCDScatteringSkillTime;
    }
    private void Skill_Scattering_Timer()//技能计时器
    {
        //散射计时
        //一开始 不能使用技能  进行技能cd的充能
        if (isCanUseScatteringSkill)
        {
            if (IsUseScatteringSkill)//是否激活了技能的使用
            {
                //倒计时
                curScatteringTime -= Time.deltaTime;
                if (curScatteringTime <= 0f)
                {
                    //技能使用时间结束
                    curScatteringTime = 0;
                    //设置不能使用技能 
                    isCanUseScatteringSkill = false;
                    //使用技能结束
                    IsUseScatteringSkill = false;
                }
                //设置技能使用的进度显示
                mainSys.SetScatteringSkillCD(curScatteringTime / maxUseScatteringSkillTime);
            }
        }
        else
        {
            curScatteringTime += Time.deltaTime;
            if (curScatteringTime >= maxCDScatteringSkillTime)
            {
                //技能已经充能好了,可以使用
                isCanUseScatteringSkill = true;
                mainSys.SetScatteringSkillMask(false);//关闭黑色遮罩
            }
            else
            {
                mainSys.SetScatteringSkillMask(true);
            }
            //设置技能冷却的进度显示
            mainSys.SetScatteringSkillCD(curScatteringTime / maxCDScatteringSkillTime);
        }
    }


    private void InitEfMapBg()//初始化背景特效
    {
        if (mapbgef == null)
        {
            mapbgef = GameObject.Find("mapbgef").GetComponent<SpriteRenderer>();
        }
        if (_mapbgef_FA == null)
        {
            _mapbgef_FA = GameObject.Find("mapbgef").GetComponent<Ef_FadeAway>();
        }
    }
    private void SetEfMapBg(string path)//设置特效背景
    {
        //直接显示/使用特效渐变
        mapbgef.sprite = ResSvc.Instance.LoadSprite(PathDefine.MapBg + dataSvc.pd.FishSceneLv.ToString() + path);
    }
    private void SetEfMapBgState(bool state)
    {
        if (state)
        {
            _mapbgef_FA.PlayBackwards();
        }
        else
        {
            _mapbgef_FA.Play();
        }
    }
}
