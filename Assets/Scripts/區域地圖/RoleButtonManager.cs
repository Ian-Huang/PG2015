using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoleButtonManager : MonoBehaviour
{
    public List<Transform> SaveRoleButtonTransformList; //記錄角色按鈕Transform清單
    public GameObject ChoosePropertiesObject;

    private List<Vector3> roleButtonPositionList;
    public static RoleButtonManager script;

    void Awake()
    {
        script = this;
    }

    // Use this for initialization
    void Start()
    {
        //記錄角色按鈕的位置資訊，並額外儲存至另一個List
        this.roleButtonPositionList = new List<Vector3>();
        foreach (Transform temp in this.SaveRoleButtonTransformList)
            this.roleButtonPositionList.Add(temp.position);

        //提供一個暫時的字典清單，儲存場景上有RoleButton(六位角色資訊)，其代表的系統角色與GameObject   。 (方便下面搜尋使用)
        Dictionary<GameDefinition.SystemPlayerName, GameObject> tempDic = new Dictionary<GameDefinition.SystemPlayerName, GameObject>();
        foreach (RoleButton script in GameObject.FindObjectsOfType<RoleButton>())
            tempDic.Add(script.SystemName, script.gameObject);

        //確認系統儲存的真實角色名字清單
        int count = 0;
        foreach (var temp in GameDefinition.PlayerNameData)
        {
            //假如為空，代表不使用此角色，將從畫面上刪除
            if (temp.Value == string.Empty)
            {
                Destroy(tempDic[temp.Key].gameObject);
            }
            //反之，代表會使用此角色，將角色按鈕移動至設定的第 count 位置 (count 從0開始記數)
            else
            {
                tempDic[temp.Key].gameObject.transform.position = roleButtonPositionList[count];    //定位至預設的第count位置

                //設定ITween動畫屬性
                MoveTo tempScript = tempDic[temp.Key].gameObject.GetComponent<MoveTo>();
                tempScript.StartPoint.x = roleButtonPositionList[count].x;  //起始位置x值
                tempScript.EndPoint.x = roleButtonPositionList[count].x;    //結束位置x值
                tempScript.DelayTime += 0.1f * count;                        //Delay時間 (為了可以看得出有依序出現的感覺)
                tempScript.Move();      //開始移動

                count++;    //記數加一
            }
        }
    }
}