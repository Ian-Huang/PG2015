using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 事件集合器，集合所有事件清單，依序播放
/// </summary>
public class EventCollection : MonoBehaviour
{
    public List<GameObject> EventList;  //儲存事件清單
    public int CurrentEventIndex;       //目前事件索引

    public GameObject Special_CheckExitArea;    //特別使用，在區域地圖確認是否離開島嶼用
    public static EventCollection script;

    void Awake()
    {
        script = this;

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