using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MissionSelectCollection : MonoBehaviour
{
    public List<IslandMissionInfo> IslandMissionList;   //各島嶼任務資訊清單
    public IslandMissionInfo CurrentIslandMissionInfo;  //當前島嶼任務資訊

    // Use this for initialization
    void Start()
    {
        //找出目前正在進行的島嶼
        this.CurrentIslandMissionInfo = this.IslandMissionList.Find((IslandMissionInfo data) =>
        {
            return (data.Island == GameDefinition.CurrentIsland);
        });

        //開啟目前正在進行的島嶼的全部NPC
        this.CurrentIslandMissionInfo.IslandNPCs.SetActive(true);

        //確認主線任務NPC是否已經進行
        if (!GameDefinition.MissionActiveStateMapping[this.CurrentIslandMissionInfo.MainMission])
        {
            //未進行，關閉其他任務NPC
            foreach (NPC tempScript in this.CurrentIslandMissionInfo.IslandNPCs.GetComponentsInChildren<NPC>())
            {
                if (tempScript.Mission != this.CurrentIslandMissionInfo.MainMission)
                    tempScript.gameObject.SetActive(false);
            }
        }
        else
        {
            //已進行，開啟其他任務NPC。同時確認已進行過的任務NPC不再開啟
            foreach (NPC tempScript in this.CurrentIslandMissionInfo.IslandNPCs.GetComponentsInChildren<NPC>())
            {
                if (GameDefinition.MissionActiveStateMapping[tempScript.Mission])
                    tempScript.gameObject.SetActive(false);
            }

            //特例任務，有些任務必須先執行過前置任務才能被觸發
            foreach (MissionRelation tempScript in this.CurrentIslandMissionInfo.MissionRelations)
            {
                //先確認前置任務是否已被觸發 (PredecessorMission)
                if (!GameDefinition.MissionActiveStateMapping[tempScript.PredecessorMission])
                {
                    //找出NPC中要被設定的NPC任務 (SetMission)
                    this.CurrentIslandMissionInfo.IslandNPCs.GetComponentsInChildren<NPC>().ToList().Find((NPC t) =>
                        {
                            return (t.Mission == tempScript.SetMission);
                        }).gameObject.SetActive(false);
                }
            }
        }
    }

    [System.Serializable]
    public class IslandMissionInfo
    {
        public GameDefinition.Island Island;
        public GameObject IslandNPCs;
        public GameDefinition.Mission MainMission;
        public List<MissionRelation> MissionRelations;
    }

    [System.Serializable]
    public class MissionRelation
    {
        public GameDefinition.Mission SetMission;
        public GameDefinition.Mission PredecessorMission;
    }
}
