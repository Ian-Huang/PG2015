using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapChangeController : MonoBehaviour
{
    public List<MapChangeInfo> MapChangeInfoList;   //各島嶼轉換過程事件清單

    // Use this for initialization
    void Start()
    {
        //找出目前即將進入的島嶼
        this.MapChangeInfoList.Find((MapChangeInfo data) =>
        {
            return (data.Island == GameDefinition.CurrentIsland);
        }).MapChangeEvents.SetActive(true);

        //修改島嶼遊戲進行時間(15分鐘)
        GameDefinition.CurrentGameTime = GameDefinition.AreaMaxGameTime;
    }

    [System.Serializable]
    public class MapChangeInfo
    {
        public GameDefinition.Island Island;
        public GameObject MapChangeEvents;
    }
}