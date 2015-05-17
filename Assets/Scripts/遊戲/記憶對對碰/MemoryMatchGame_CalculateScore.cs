using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MemoryMatchGame_CalculateScore : MonoBehaviour
{
    public List<Transform> PositionList;
    public List<GameDefinition.RoleData> RoleDataList;

    private int currentIndex;

    // Use this for initialization
    IEnumerator Start()
    {
        this.currentIndex = 0;
        foreach (var temp in GameCollection.script.MemoryGameRoleScoreMapping)
        {
            //找出目前要處理的腳色
            GameDefinition.RoleData tempRoleData = this.RoleDataList.Find((GameDefinition.RoleData data) =>
              {
                  return (data.SystemName == temp.Key);
              });
            //開啟腳色物件，並放置於預先儲存的位置，同時顯示計分
            tempRoleData.RoleObject.SetActive(true);
            tempRoleData.RoleObject.transform.position = this.PositionList[this.currentIndex].position;
            tempRoleData.RoleObject.GetComponentInChildren<ScoreValueTo>().EndValue = temp.Value * 100;
            tempRoleData.RoleObject.GetComponentInChildren<ScoreValueTo>().StartRun();
            this.currentIndex++;
            yield return new WaitForSeconds(0.5f);
        }
    }
}