using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 腳色動畫集合
/// </summary>
public class RoleAnimationCollection : MonoBehaviour
{
    public List<GameDefinition.RoleData> RoleDataList; //腳色動畫資訊清單
    public GameObject CurrentCloneRoleObject;   //目前對話腳色(複製)

    public static RoleAnimationCollection script;

    void Awake()
    {
        script = this;
    }

    /// <summary>
    /// 開啟針對指定腳色動畫
    /// </summary>
    /// <param name="name">系統腳色名</param>
    public void RoleAppear(GameDefinition.SystemPlayerName name)
    {
        this.RoleDataList.Find((GameDefinition.RoleData data) =>
        {
            if (data.SystemName == name)
            {
                this.CurrentCloneRoleObject = Instantiate(data.RoleObject) as GameObject;
                this.CurrentCloneRoleObject.transform.parent = this.transform;
                this.CurrentCloneRoleObject.SetActive(true);
                return true;
            }
            else
                return false;
        });
    }

    public void RoleDisappear()
    {
        Destroy(this.CurrentCloneRoleObject);
    }
}