/**
*Copyright(C) 2019 by #COMPANY#
*All rights reserved.
*FileName:     #SCRIPTFULLNAME#
*Author:       #AUTHOR#
*Version:      #VERSION#
*UnityVersion：#UNITYVERSION#
*Date:         #DATE#
*Description:   管理设置参数
* 分辨率设置
* 清晰度设置
*History:
*/
using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine;

public class SetUpManager
{
    /// <summary>
    /// 储存所需要的分辨率 我这里储存16：9 
    /// </summary>
    public enum EResolution
    {
        _1280x720 = 0,
        _1600x900,
        _1920x1080,
        All,
    }

    /// <summary>
    /// 储存所需要的画质等级 对应QualitySettings.names的index 从0开始
    /// </summary>
    public enum EQualityLevel
    {
        Low = 2,
        Medium = 3,
        Hight = 4
    }

    ////官方的无法直接调用 没有反应要自己写
    //Resolution[] resolutions;
    //public SetUpManager()
    //{
    //    resolutions = Screen.resolutions;
    //}

    Resolution[] resolutions = new Resolution[3] {
        new Resolution { width=1280, height=720, refreshRate=60 },
        new Resolution { width=1600, height=900, refreshRate=60 },
        new Resolution { width=1920, height=1080, refreshRate=60}
    };

    /// <summary>
    /// 设置分辨率
    /// </summary>
    public IEnumerator SetResolution(EResolution type)
    {
        Resolution res;
        //全屏
        if (type == EResolution.All)
        {
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true, 60);
            yield return null;
        }
        //所有可以设置的分辨率 unity自带
        res = resolutions[(int)type];
        //如果播放器是运行在窗口模式，则返回的是当前桌面的分辨率  可使用分辨率不能大于这个数
        //  Screen.currentResolution 
        //全屏切回来会在左上角 所以先取消全屏 因为这些函数都是在这一帧结束时调用 就等待它调用完毕再设置
        yield return Screen.fullScreen=false;
       
        Screen.SetResolution(res.width, res.height, false,res.refreshRate);      
    }

    /// <summary>
    /// 设置画面质量
    /// </summary>
    public void SetDrawQuality(EQualityLevel type)
    {
        //索引 是否显示高画质(false之后改变画质有些消耗资源参数可能不变 所以改为true)
        QualitySettings.SetQualityLevel((int)type, true);
    }

    /// <summary>
    /// 获取当前分辨率
    /// </summary>
    public EResolution GetNowResolution()
    {
        for (int i = 0; i < resolutions.Length; i++)
        {
            var res = resolutions[i];
            //等于返回0，大于返回大于0的值，小于返回小于0的值 基本是-1 0 1
            if (res.width.CompareTo(Screen.width) == 0 && res.height.CompareTo(Screen.height) == 0 && !IsFullScreen())
            {
                return (EResolution)i;
            }
        }
        return EResolution.All;
    }

    /// <summary>
    /// 是否全屏
    /// </summary>
    /// <returns></returns>
    public bool IsFullScreen()
    {
        return Screen.fullScreen;
    }

    /// <summary>
    /// 获取当前画质
    /// </summary>
    /// <returns></returns>
    public EQualityLevel GetNowDrawQuality()
    {
        return (EQualityLevel)QualitySettings.GetQualityLevel();
    }
}
