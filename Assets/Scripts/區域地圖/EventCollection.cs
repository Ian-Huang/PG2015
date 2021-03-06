﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 事件集合器，集合所有事件清單，依序播放
/// </summary>
public class EventCollection : MonoBehaviour
{
    [HideInInspector]
    public List<GameObject> EventList;  //儲存事件清單

    public List<GameObject> JumpStroyList;  //儲存跳關功能目標故事物件清單

    public bool isDebugMode = false;
    public GameObject StartEvent;       //測試用，開始事件

    public int CurrentEventIndex;       //目前事件索引 
    public GameObject Special_CheckExitArea;    //特別使用，  2014-在區域地圖確認是否離開島嶼用 , 2015-按下開始遊戲後閃光
    public static EventCollection script;

    void Awake()
    {
        script = this;

        for (int i = 0; i < this.transform.childCount; i++)
        {
            //debug模式 透過自選Event跳躍，反之則特過特定事件點跳躍
            if (this.isDebugMode)
            {
                //判定是否使用測試開始事件
                if (this.StartEvent != null)
                {
                    if (this.StartEvent == this.transform.GetChild(i).gameObject)
                        this.CurrentEventIndex = i;
                }
            }
            else
            {
                if (this.JumpStroyList.Count > 0)
                {
                    if (this.JumpStroyList[GameDefinition.QuickJumpStoryIndex] == this.transform.GetChild(i).gameObject)
                        this.CurrentEventIndex = i;
                }
            }

            this.EventList.Add(this.transform.GetChild(i).gameObject);
        }
        foreach (GameObject temp in this.EventList)
            temp.SetActive(false);

        this.EventList[this.CurrentEventIndex].SetActive(true);
    }

    /// <summary>
    /// 切換至下一事件
    /// </summary>
    public void NextEvent()
    {
        this.EventList[this.CurrentEventIndex].SetActive(false);    //關閉前一事件物件
        this.CurrentEventIndex++;
        this.EventList[this.CurrentEventIndex].SetActive(true);     //開啟新一事件物件
    }

    public void NextEvent(int index)
    {
        this.EventList[this.CurrentEventIndex].SetActive(false);    //關閉前一事件物件
        this.CurrentEventIndex = index;
        this.EventList[this.CurrentEventIndex].SetActive(true);     //開啟新一事件物件
    }

    /// <summary>
    /// 切換至上一事件
    /// </summary>
    public void BackEvent()
    {
        this.EventList[this.CurrentEventIndex].SetActive(false);    //關閉目前事件物件
        this.CurrentEventIndex--;
        this.EventList[this.CurrentEventIndex].SetActive(true);     //開啟前一事件物件
    }
}