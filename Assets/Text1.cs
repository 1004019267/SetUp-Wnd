/**
 *Copyright(C) 2019 by #COMPANY#
 *All rights reserved.
 *FileName:     #SCRIPTFULLNAME#
 *Author:       #AUTHOR#
 *Version:      #VERSION#
 *UnityVersion：#UNITYVERSION#
 *Date:         #DATE#
 *Description:   
 *History:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Text1 : MonoBehaviour
{
    ToggleGroup resGroup;
    ToggleGroup quaGroup;
    SetUpManager set = new SetUpManager();

    void Awake()
    {
        ////可以打印出来去选择自己需要的分辨率 是根据 Screen.currentResolution 的限制改变可用的种类
        //foreach (Resolution item in Screen.resolutions)
        //{
        //    Debug.Log(item.width + "x" + item.height);
        //}

        //打印当前等级
        //foreach (var item in QualitySettings.names)
        //{
        //    Debug.Log(item.ToString());
        //}

        resGroup = transform.Find("GameObject").GetComponent<ToggleGroup>();
        quaGroup = transform.Find("GameObject (1)").GetComponent<ToggleGroup>();
        foreach (var item in resGroup.GetComponentsInChildren<Toggle>())
        {
            item.onValueChanged.AddListener((isOn) =>
            {
                if (isOn)
                {
                 StartCoroutine(set.SetResolution((SetUpManager.EResolution)int.Parse(item.name)));
                }
            });
        }
        foreach (var item in quaGroup.GetComponentsInChildren<Toggle>())
        {
            item.onValueChanged.AddListener((isOn) =>
            {
                if (isOn)
                {
                    set.SetDrawQuality((SetUpManager.EQualityLevel)int.Parse(item.name));
                }
            });
        }
    }

    void OnEnable()
    {
        RefreshBtn();
    }
    // 由于unity自带配置表记录用户数据我们要实时改变第二次及以后打开的Btn位置
    /// <summary>
    ///刷新Btn 
    /// </summary>
    void RefreshBtn()
    {
        foreach (var item in resGroup.GetComponentsInChildren<Toggle>())
        {
            if (item.name == ((int)set.GetNowResolution()).ToString())
            {
                item.isOn = true;
            }
        }

        foreach (var item in quaGroup.GetComponentsInChildren<Toggle>())
        {
            if (item.name == ((int)set.GetNowDrawQuality()).ToString())
            {
                item.isOn = true;
            }
        }
    }
}


