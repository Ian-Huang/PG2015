using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IslandEndTalkingController : MonoBehaviour
{
    public List<IslandEndTalking> IslandEndTalkingList;   //各島嶼事件清單
    private IslandEndTalking IslandEndTalkingData;

    // Use this for initialization
    void Start()
    {
        //找出目前即將進入的島嶼
        this.IslandEndTalkingData = this.IslandEndTalkingList.Find((IslandEndTalking data) =>
           {
               return (data.Island == GameDefinition.CurrentIsland);
           });

        this.IslandEndTalkingData.IslandEndTalkingEvents.SetActive(true);
        GameDefinition.CurrentIsland = this.IslandEndTalkingData.NextIsland;
    }

    [System.Serializable]
    public class IslandEndTalking
    {
        public GameDefinition.Island Island;
        public GameObject IslandEndTalkingEvents;
        public GameDefinition.Island NextIsland;
    }
}
